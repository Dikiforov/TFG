## README.md

**Vivienda Inteligente - Proyecto en Unity**

Este proyecto implementa una vivienda de prueba en Unity, ambientada en su propia vivienda, con una serie de sensores para monitorizar diferentes aspectos del hogar.

**Sensores:**

* **Movimiento:** Detección de movimiento dentro de la vivienda.
* **Puertas:** Monitorización del estado de las puertas (abiertas/cerradas).
* **Humedad:** Medición del nivel de humedad en el ambiente.
* **Temperatura:** Control de la temperatura ambiente.
* **Luminosidad:** Detección del nivel de luz en la vivienda.
* **Sonido:** Captura de eventos sonoros.

**Flujo de datos:**

1. Los sensores envían información a través de un puerto local del ordenador.
2. Un receptor de datos normaliza la información y la envía al servidor.
3. El servidor almacena los datos en la base de datos mediante una API.
4. Una página web con un backend realiza peticiones a la API para visualizar la información.
5. Los usuarios pueden iniciar sesión y ver diferentes datos del sistema.

**Fecha de avance:**

* **15/02/2024**
Se ha creado el repositorio de GitHub para compartir los avances de código y no perderlos.

**Avances futuros:**
Añadir el trabajo realizado en otros días.

**Este proyecto se encuentra en desarrollo y se irán actualizando los avances en este archivo README.md.**
