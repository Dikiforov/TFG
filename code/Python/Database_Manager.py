import sqlite3

class DatabaseManager:
    def __init__(self, db_path):
        self.db_path = db_path
        self.connection = None
        self.cursor = None

    def connect(self):
        self.connection = sqlite3.connect(self.db_path)
        self.cursor = self.connection.cursor()

    def close(self):
        if self.connection:
            self.connection.close()

    def create_tables(self):
        try:
            self.connect()
            with open("esquema_db.sql", "r") as f:
                self.cursor.executescript(f.read())
            self.connection.commit()
        except sqlite3.Error as e:
            print(f"Error al crear las tablas: {e}")
        finally:
            self.close()

    def execute_query(self, query, params=None):
        try:
            self.connect()
            if params:
                self.cursor.execute(query, params)
            else:
                self.cursor.execute(query)
            self.connection.commit()
            return self.cursor.fetchall()
        except sqlite3.Error as e:
            print(f"Error al ejecutar la consulta: {e}")
        finally:
            self.close()

    # --- Operaciones CRUD ---

    def create(self, table, data):
        columns = ", ".join(data.keys())
        placeholders = ", ".join("?" * len(data))
        query = f"INSERT INTO {table} ({columns}) VALUES ({placeholders})"
        self.execute_query(query, tuple(data.values()))

    def read(self, table, columns="*", condition=None):
        query = f"SELECT {columns} FROM {table}"
        if condition:
            query += f" WHERE {condition}"
        return self.execute_query(query)

    def update(self, table, data, condition):
        updates = ", ".join([f"{col} = ?" for col in data])
        query = f"UPDATE {table} SET {updates} WHERE {condition}"
        self.execute_query(query, tuple(data.values()))

    def delete(self, table, condition):
        query = f"DELETE FROM {table} WHERE {condition}"
        self.execute_query(query)

    # --- Método específico para insertar lecturas de sensor ---

    def insert_sensor_reading(self, user_id, sensor_type, value, room, hour):
        try:
            self.connect()  # Asegurarse de conectar antes de ejecutar la consulta
            value = float(str(value).replace(',', '.'))
            
            self.cursor.execute(
                """
                INSERT INTO sensor_readings (user_id, sensor_type, value, room, hour)
                VALUES (?, ?, ?, ?, ?)
                """,
                (user_id, sensor_type, value, room, hour)
            )
            self.connection.commit()
        except sqlite3.IntegrityError:
            pass
        except sqlite3.Error as e:
            print(f"Error al insertar lectura de sensor: {e}")
        finally:
            self.close() 
