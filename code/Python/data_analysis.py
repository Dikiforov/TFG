import pandas as pd
import numpy as np
import sqlite3
import matplotlib.pyplot as plt
import seaborn as sns
from scipy.stats import zscore
from sklearn.cluster import KMeans

# Conexión a la base de datos
ruta_datos = "C:\\Users\\Daniil\\Documents\\GitHub\\TFG\\Datos"
db_path = f"{ruta_datos}\\InformacionSistema.db"
conn = sqlite3.connect(db_path)

# Consulta SQL para obtener datos de todas las habitaciones
query = """
SELECT sensor_readings.date, sensor_readings.sensor_type, sensor_readings.value, sensor_readings.room 
FROM sensor_readings
"""

# Cargar datos en un DataFrame
df = pd.read_sql_query(query, conn)

# Verificar si hay datos antes de continuar
if df.empty:
    print("No hay datos disponibles para los sensores seleccionados.")
else:
    # Procesamiento y análisis de datos (modificaciones aquí)
    df['date'] = pd.to_datetime(df['date']) 
    df.set_index('date', inplace=True)

    # Ordenar por fecha
    df = df.sort_values(by='date') 

    # Agrupar por habitación y realizar análisis para cada una
    for room, df_room in df.groupby('room'):
        print(f"\nAnálisis para la habitación: {room}")

        # Ventanas temporales deslizantes (Ejemplo: media móvil de 3 días)
        df_rolling = df_room.groupby('sensor_type')['value'].rolling('3D').mean().reset_index()

        # Detección de anomalías (Ejemplo: basado en z-score)
        df_zscore = df_room.groupby('sensor_type')['value'].transform(lambda x: zscore(x))
        df_room['anomaly'] = df_zscore.abs() > 2

        # Clustering (Ejemplo: K-means con 3 clusters)
        cluster_data = df_room.groupby('sensor_type')['value'].mean().values.reshape(-1, 1)
        if cluster_data.shape[0] > 0:
            kmeans = KMeans(n_clusters=3, random_state=0).fit(cluster_data)
            df_room['cluster'] = kmeans.labels_[df_room['sensor_type'].map({'temperature': 0, 'luminosity': 1, 'humidity': 2, 'movement': 3, 'doors': 4, 'sound': 5})]  # Agregado 'doors' y 'sound'
        else:
            print("No hay suficientes datos para el clustering en esta habitación.")

        # Visualizaciones

        # Diagramas de dispersión 
        if not df_room.empty:
            sns.pairplot(df_room, hue='sensor_type')
            plt.title(f"Diagramas de dispersión para {room}")
            plt.show()
        else:
            print(f"No hay datos para el diagrama de dispersión en {room}.")


        # Mapas de calor (Ejemplo: temperatura a lo largo del tiempo)
        df_temp = df_room[df_room['sensor_type'] == 'temperature']
        if not df_temp.empty:
            df_pivot = df_temp.pivot_table(index=df_temp.index.date, columns=df_temp.index.time, values='value')
            sns.heatmap(df_pivot)
            plt.xlabel('Hora')
            plt.ylabel('Fecha')
            plt.title(f'Mapa de calor de temperatura en {room}')
            plt.show()
        else:
            print(f"No hay datos de temperatura para el mapa de calor en {room}.")

conn.close()
