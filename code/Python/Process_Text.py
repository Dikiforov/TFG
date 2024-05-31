import json
import os

# Rutas de los archivos
ruta_fichero_texto = "C:\\Users\\Daniil\\Documents\\GitHub\\TFG\\Datos\\datos_sensor.txt"
ruta_datos = "C:\\Users\\Daniil\\Documents\\GitHub\\TFG\\Datos"  # Directorio para almacenar JSONs

# Función para procesar una línea y convertirla a JSON
def procesar_linea(linea):
    datos = {}
    placaTiempo, mediciones = linea.split("-")
    placa, timestamp = placaTiempo.split(" ")
    mediciones = mediciones.split(";")

    for medicion in mediciones:
        print(medicion)
        clave, valor = medicion.split("=")
        if clave == "Puertas":
            # Procesar el valor de Puertas (asumimos un formato específico)
            puertas = {}
            if len(valor.split(",")) > 1:
                for puerta in valor.split(","):
                    nombre, estado = puerta.split(":")
                    puertas[nombre] = estado.lower() == "true"  # Convertir a booleano
            valor = puertas
        elif clave in ["Temperatura", "Humedad"]:
            # Reemplazar "," por "." en valores numéricos
            valor = valor.replace(",", ".")
            try:
                valor = float(valor)  # Convertir a número si es posible
            except ValueError:
                pass  # Mantener como cadena si no es convertible

        datos[clave] = valor

    return placa, timestamp, datos

# Diccionario para almacenar los datos de todas las placas
datos_placas = {}

# Leer y procesar el fichero de texto línea por línea
with open(ruta_fichero_texto, "r") as archivo_texto:
    for linea in archivo_texto:
        placa, timestamp, datos = procesar_linea(linea)

        # Agregar los datos al diccionario correspondiente a la placa
        if placa not in datos_placas:
            datos_placas[placa] = {}
        datos_placas[placa][timestamp] = datos

# Escribir los datos de cada placa en un archivo JSON separado
for placa, datos in datos_placas.items():
    ruta_json = os.path.join(ruta_datos, f"{placa}.json")
    with open(ruta_json, "w") as archivo_json:
        json.dump(datos, archivo_json, indent=4)  # Indentación para mejor legibilidad
