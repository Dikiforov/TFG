import sqlite3
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
from scipy.stats import zscore  # Para detección de anomalías
from sklearn.cluster import KMeans  # Para clustering
from datetime import timedelta, datetime
import seaborn as sns  # Para gráficos más elaborados
import os

class DataAnalyzer:
    def __init__(self, db_path):
        self.db = sqlite3.connect(db_path)

    def close(self):
        self.db.close()

    def execute_query(self, query):
        return pd.read_sql_query(query, self.db)

    def sliding_windows(self, sensor_type, window_size, step_size=None, time_window=True):
        df = self.execute_query(f"SELECT * FROM sensor_readings WHERE sensor_type = '{sensor_type}'")

        # Obtener la fecha de hoy 
        today = datetime.now().date()

        # Combinar la fecha de hoy con la hora de la base de datos
        df['hour'] = pd.to_datetime(today.strftime('%Y-%m-%d') + ' ' + df['hour'], format="%Y-%m-%d %H:%M:%S")

        # Convertir 'value' a numérico, ignorando errores de conversión
        df['value'] = pd.to_numeric(df['value'], errors='coerce')

        # Eliminar filas con valores NaN en 'value'
        df.dropna(subset=['value'], inplace=True)

        df = df.sort_values('hour')
        df.set_index('hour', inplace=True)

        if time_window:
            window = window_size
        else:
            window = int(window_size)

        if step_size:
            return df.rolling(window).agg(['mean', 'std']).resample(step_size).mean()
        else:
            return df.rolling(window).agg(['mean', 'std'])

    def anomaly_detection(self, sensor_type, threshold=3):
        df = self.execute_query(f"SELECT * FROM sensor_readings WHERE sensor_type = '{sensor_type}'")

        # Obtener la fecha de hoy 
        today = datetime.now().date()

        # Combinar la fecha de hoy con la hora de la base de datos
        df['hour'] = pd.to_datetime(today.strftime('%Y-%m-%d') + ' ' + df['hour'], format="%Y-%m-%d %H:%M:%S")

        # Convertir 'value' a numérico, ignorando errores de conversión
        df['value'] = pd.to_numeric(df['value'], errors='coerce')

        # Eliminar filas con valores NaN en 'value'
        df.dropna(subset=['value'], inplace=True)

        df = df.sort_values('hour')
        df.set_index('hour', inplace=True)

        df['z_score'] = zscore(df['value'])
        return df[abs(df['z_score']) > threshold]


    def clustering(self, sensor_types):
        df = self.execute_query(f"SELECT * FROM sensor_readings WHERE sensor_type IN {tuple(sensor_types)}")
        kmeans = KMeans(n_clusters=3)
        df['cluster'] = kmeans.fit_predict(df[['value']])
        return df

    def dynamic_plots(self, sensor_type):
        df = self.execute_query(f"SELECT * FROM sensor_readings WHERE sensor_type = '{sensor_type}'")
        plt.plot(df['hour'], df['value'])
        plt.xlabel('Time')
        plt.ylabel(sensor_type)
        plt.title(f'Dynamic Plot of {sensor_type}')
        plt.show()

    def scatter_plot(self, sensor_type_x, sensor_type_y):
        df = self.execute_query(f"SELECT * FROM sensor_readings WHERE sensor_type IN ('{sensor_type_x}', '{sensor_type_y}')")
        sns.scatterplot(data=df, x=sensor_type_x, y=sensor_type_y, hue='room')
        plt.title(f'Scatter Plot of {sensor_type_x} vs {sensor_type_y}')
        plt.show()

    def heatmap(self, sensor_type):
        df = self.execute_query(f"SELECT room, hour, AVG(value) as avg_value FROM sensor_readings WHERE sensor_type = '{sensor_type}' GROUP BY room, hour")
        df['hour'] = pd.to_datetime(df['hour'])
        df_pivot = df.pivot_table(index='room', columns='hour', values='avg_value')
        sns.heatmap(df_pivot)
        plt.title(f'Heatmap of {sensor_type}')
        plt.show()

ruta_datos = "C:\\Users\\Daniil\\Documents\\GitHub\\TFG\\Datos"
db_path = f"{ruta_datos}\\InformacionSistema.db"
analyzer = DataAnalyzer(db_path)
#result = analyzer.sliding_windows('temperature', '1H', '15min')  
#print(result)

nomalies = analyzer.anomaly_detection('humidity', 67)
print(nomalies)
print("-------------------------------------------------------------")
clusters = analyzer.clustering(['temperature', 'humidity'])
print(clusters)
