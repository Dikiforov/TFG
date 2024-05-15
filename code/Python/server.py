import socket
import threading
import time
from queue import Queue
from datetime import datetime
from tabulate import tabulate
import sqlite3
import os
# Puerto para la recepción de datos
puerto = 1234
ip_addr = "0.0.0.0"
# Diccionario para almacenar las colas por tipo de sensor

sensor_queues = {
    'Puertas': Queue(),
    'Movimiento': Queue(),
    'Temperatura': Queue(),
    'Humedad': Queue(),
    'Sonido': Queue(),
    'Presion': Queue(),
    'Luminosidad': Queue(),
    'Hora': Queue(),
}

# Diccionario que contendrá cada habitación asociada a su conjunto de sensores
room_queues = {
    'PlacaRecibidor': dict(sensor_queues),
    'PlacaSalonComedor': dict(sensor_queues),
    'PlacaCocina': dict(sensor_queues),
    'PlacaHabitacion_1': dict(sensor_queues),
    'PlacaHabitacion_2': dict(sensor_queues),
    'PlacaHabitacion_3': dict(sensor_queues),
    'PlacaHabitacion_4': dict(sensor_queues),
    'PlacaServicio': dict(sensor_queues),
    'PlacaPasillo': dict(sensor_queues)
}

def handle_client(client_socket):
    # Formato del mensaje entrante: room hora;sensor_type:data_sensor,...
    request = client_socket.recv(1024).decode('utf-8').rstrip()
    roomAndHour, sensorData = request.split(';') # Los datos nos llegan en forma -> habitación Hora;datos_sensores
    room, hour = roomAndHour.split(' ') # Obtención de la habitación y hora de actualización
    lista_sensores = sensorData.split(',')

    # Diccionario para almacenar los datos de los sensores por habitación
    sensor_dict = {}
    
    # Recopilar datos de los sensores para cada habitación
    for sensor in lista_sensores:
        try:
            if sensor != '':
                sensor_type, sensor_value = sensor.split(':')
                if sensor_value.lower() == 'true':
                    sensor_value = 1
                elif sensor_value.lower() == 'false':
                    sensor_value = 0
                if room not in sensor_dict:
                    sensor_dict[room] = {}
                if sensor_type not in sensor_dict[room]:
                    sensor_dict[room][sensor_type] = sensor_value
        except ValueError:
            # Si el split falla, imprimir el mensaje del request directamente
            print(request)
            break  # Salir del bucle para evitar procesar más datos

    print(request)

    # Conectar a la base de datos
    conn = sqlite3.connect('sensores.db')
    cursor = conn.cursor()

    # Insertar los datos en la tabla 'sensor_readings'
    for room, sensors in sensor_dict.items():
        cursor.execute('''INSERT INTO sensor_readings (user_id, sensor_type, value, room, hour)
                          VALUES (?, ?, ?, ?, ?)''',
                       (1,  # ID del usuario (cambiar según sea necesario)
                        'Temperatura', sensors.get('Temperatura', None), room, hour))
        cursor.execute('''INSERT INTO sensor_readings (user_id, sensor_type, value, room, hour)
                          VALUES (?, ?, ?, ?, ?)''',
                       (1,  # ID del usuario (cambiar según sea necesario)
                        'Puertas', sensors.get('Puertas', None), room, hour))
        cursor.execute('''INSERT INTO sensor_readings (user_id, sensor_type, value, room, hour)
                          VALUES (?, ?, ?, ?, ?)''',
                       (1,  # ID del usuario (cambiar según sea necesario)
                        'Luminosidad', sensors.get('Luminosidad', None), room, hour))
        cursor.execute('''INSERT INTO sensor_readings (user_id, sensor_type, value, room, hour)
                          VALUES (?, ?, ?, ?, ?)''',
                       (1,  # ID del usuario (cambiar según sea necesario)
                        'Movimiento', sensors.get('Movimiento', None), room, hour))
        cursor.execute('''INSERT INTO sensor_readings (user_id, sensor_type, value, room, hour)
                          VALUES (?, ?, ?, ?, ?)''',
                       (1,  # ID del usuario (cambiar según sea necesario)
                        'Sonido', sensors.get('Sonido', None), room, hour))
        cursor.execute('''INSERT INTO sensor_readings (user_id, sensor_type, value, room, hour)
                          VALUES (?, ?, ?, ?, ?)''',
                       (1,  # ID del usuario (cambiar según sea necesario)
                        'Presion', sensors.get('Presion', None), room, hour))
        cursor.execute('''INSERT INTO sensor_readings (user_id, sensor_type, value, room, hour)
                          VALUES (?, ?, ?, ?, ?)''',
                       (1,  # ID del usuario (cambiar según sea necesario)
                        'Humedad', sensors.get('Humedad', None), room, hour))

    conn.commit()
    conn.close()
    client_socket.close()


def server():
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind((ip_addr, puerto))
    server_socket.listen(5)
    print(f"El servidor está escuchando en el puerto {puerto}")

    while True:
        client_socket, addr = server_socket.accept()
        client_handler = threading.Thread(target=handle_client, args=(client_socket,))
        client_handler.start()

def create_db():
    # Si no existe, crearla y las tablas correspondientes
    conn = sqlite3.connect('sensores.db')
    cursor = conn.cursor()

    # Crear tabla 'users'
    cursor.execute('''CREATE TABLE users (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            username VARCHAR,
                            password VARCHAR,
                            address VARCHAR,
                            region VARCHAR,
                            zip_code INTEGER,
                            water_supply INTEGER,
                            gas_supply INTEGER,
                            light_supply INTEGER
                        )''')

    # Crear tabla 'sensor_readings'
    cursor.execute('''CREATE TABLE sensor_readings (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            user_id INTEGER,
                            sensor_type VARCHAR,
                            value FLOAT,
                            room VARCHAR,
                            hour TIMESTAMP,
                            FOREIGN KEY (user_id) REFERENCES users(id)
                        )''')

        # Crear tabla 'supplies'
    cursor.execute('''CREATE TABLE supplies (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            name VARCHAR,
                            unit VARCHAR,
                            cost_unit FLOAT,
                            type VARCHAR
                        )''')

        # Crear tabla 'consumption'
    cursor.execute('''CREATE TABLE consumption (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            date TIMESTAMP,
                            supply_id INTEGER,
                            value FLOAT,
                            cost FLOAT,
                            FOREIGN KEY (supply_id) REFERENCES supplies(id)
                        )''')

        # Crear tabla 'daily_consumption'
    cursor.execute('''CREATE TABLE daily_consumption (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            date TIMESTAMP,
                            supply_id INTEGER,
                            value FLOAT,
                            cost FLOAT,
                            FOREIGN KEY (supply_id) REFERENCES supplies(id)
                        )''')

        # Crear tabla 'period'
    cursor.execute('''CREATE TABLE period (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            name VARCHAR,
                            initial_date TIMESTAMP,
                            final_date TIMESTAMP
                        )''')

        # Crear tabla 'period_consumption'
    cursor.execute('''CREATE TABLE period_consumption (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            period_id INTEGER,
                            supply_id INTEGER,
                            total_value FLOAT,
                            total_cost FLOAT,
                            FOREIGN KEY (period_id) REFERENCES period(id),
                            FOREIGN KEY (supply_id) REFERENCES supplies(id)
                        )''')

    conn.commit()
    conn.close()
if not os.path.exists('sensores.db'):
    create_db()
# Iniciar el servidor y la función de impresión en hilos separados
server_thread = threading.Thread(target=server)
server_thread.start()
