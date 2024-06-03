from datetime import datetime 
import os
import re
from Database_Manager import DatabaseManager

def process_data_file(file_path, db_manager, user_id):
    if not os.path.exists(db_manager.db_path):
        db_manager.create_tables()  # Crear tablas si no existen

    #user_id = int(file_path.split("_")[1].split(".")[0])

    with open(file_path, 'r') as file:
        for line in file:
            # Expresión regular para extraer datos
            pattern = r"Placa(\w+) (\d{2}/\d{2}/\d{4} \d{2}:\d{2}:\d{2})-Temperatura=([\d,.]+);Puertas=(.*?);Luminosidad=(\d+);Movimiento=(\w+);Humedad=(\d+)"
            match = re.match(pattern, line)

            if match:
                room, date_str, temperature, doors, luminosity, movement, humidity = match.groups()
                date_time = datetime.strptime(date_str, '%d/%m/%Y %H:%M:%S') 

                # Convertir valores a los tipos adecuados
                luminosity = int(luminosity)
                humidity = int(humidity)
                temperature = float(temperature.replace(",", "."))
                movement = 1 if movement.lower() == "true" else 0  # Convertir a 1 o 0

                # Insertar lecturas en la base de datos
                db_manager.insert_sensor_reading(user_id, "temperature", temperature, room, date_time)
                db_manager.insert_sensor_reading(user_id, "luminosity", luminosity, room, date_time)
                db_manager.insert_sensor_reading(user_id, "humidity", humidity, room, date_time)
                db_manager.insert_sensor_reading(user_id, "movement", movement, room, date_time)
                if doors:  # Insertar datos de puertas solo si existen
                    # Extraer el nombre de la puerta y el estado
                    door_name, state_str = doors.strip('{}').split('{')  # Elimina las llaves y divide la cadena
                    state = state_str.lower() == "true"  # Convertir a booleano

                    # Insertar en la tabla door_states
                    db_manager.execute_query(
                        """
                        INSERT INTO door_states (user_id, room, door_name, state, date)
                        VALUES (?, ?, ?, ?, ?)
                        """,
                        (user_id, room, door_name, state, date_time)
                    )

def process_data_files_in_folder(folder_path, db_manager):
    for filename in os.listdir(folder_path):
        if filename.startswith("datos_") and filename.endswith(".txt"):
            file_path = os.path.join(folder_path, filename)
            process_data_file(file_path, db_manager, filename)


ruta_datos = "C:\\Users\\Daniil\\Documents\\GitHub\\TFG\\Datos"
db_path = f"{ruta_datos}\\InformacionSistema.db"
db_manager = DatabaseManager(db_path)
process_data_files_in_folder(ruta_datos, db_manager)
db_manager.close()
