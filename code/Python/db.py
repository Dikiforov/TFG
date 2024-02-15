import sqlite3
db_name = 'tfg_nikiforov.db'

def create_table(table_name, columns):
    conn = sqlite3.connect(db_name)
    c = conn.cursor()
    column_definitions = ', '.join(f'{column} TEXT' for column in columns)
    c.execute(f'''CREATE TABLE IF NOT EXISTS {table_name}
                 (id INTEGER PRIMARY KEY AUTOINCREMENT,
                  {column_definitions})''')
    conn.commit()
    conn.close()

def insert(table_name, values):
    conn = sqlite3.connect(db_name)
    c = conn.cursor()
    column_names = ', '.join(values.keys())
    placeholders = ', '.join('?' * len(values))
    c.execute(f"INSERT INTO {table_name} ({column_names}) VALUES ({placeholders})", tuple(values.values()))
    conn.commit()
    conn.close()

def read(table_name):
    conn = sqlite3.connect(db_name)
    c = conn.cursor()
    c.execute(f"SELECT * FROM {table_name}")
    rows = c.fetchall()
    conn.close()
    return rows

def update(table_name, user_id, new_values):
    conn = sqlite3.connect(db_name)
    c = conn.cursor()
    set_statements = ', '.join(f'{column} = ?' for column in new_values)
    c.execute(f"UPDATE {table_name} SET {set_statements} WHERE id=?", (*new_values.values(), user_id))
    conn.commit()
    conn.close()

def delete(table_name, user_id):
    conn = sqlite3.connect(db_name)
    c = conn.cursor()
    c.execute(f"DELETE FROM {table_name} WHERE id=?", (user_id,))
    conn.commit()
    conn.close()

if __name__ == '__main__':
    create_table('users', ['name', 'email', 'password', 'address', 'region', 'zip_code', 'water_supply', 'gas_supply', 'light_supply'])
    create_table('sensors_reading', ['user_id', 'sensor_type', 'value', 'room', 'hour'])
    insert('users', {'name': 'Daniil Nikiforov', 'age': 24})
    insert('users', {'name': 'Paquito Navarro', 'age': 28})
    insert('users', {'name': 'Federico Garcia', 'age': 33})