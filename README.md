## README.md

**Vivienda Inteligente - Proyecto en Unity**

Este proyecto implementa una vivienda de prueba en Unity, ambientada en su propia vivienda, con una serie de sensores para monitorizar diferentes aspectos del hogar.

**Sensores:**

- **Movimiento:** Detección de movimiento dentro de la vivienda.
- **Puertas:** Monitorización del estado de las puertas (abiertas/cerradas).
- **Humedad:** Medición del nivel de humedad en el ambiente.
- **Temperatura:** Control de la temperatura ambiente.
- **Luminosidad:** Detección del nivel de luz en la vivienda.
- **Sonido:** Captura de eventos sonoros.

**Flujo de datos:**

1. Los sensores envían información a través de un puerto local del ordenador.
2. Un receptor de datos normaliza la información y la envía al servidor.
3. El servidor almacena los datos en la base de datos mediante una API.
4. Una página web con un backend realiza peticiones a la API para visualizar la información.
5. Los usuarios pueden iniciar sesión y ver diferentes datos del sistema.

**Fecha de avance:**

- **30/06/2023**

Después de realizar diferentes búsquedas sobre los sensores se ha decidido lo siguiente:

Utilizar algún tipo de herramienta digital para crear un pequeño circuito y obtener valores (simulación), una de estas herramientas podría ser Unity o Unreal Engine para realizar un modelado de una vivienda junto a sus respectivos sensores.

- **03/07/2023**

  - **Busqueda de sensores:** esto permitirá hacer una simulación de cada uno de estos en base a su precio y características.
  - **Uso de los sensores:** principalmente servirá para saber como conectarlos y recopilar los datos.
  - **Creación de la base de datos:** servirá para recopilar en un mismo lugar todos los datos obtenidos por los sensores en unity y, posteriormente, mostrar los resultados en una futura página web.
  - **Programa Sensor-BD:** programa que irá recopilando los datos de los sensores y normalizandolos.
  - **BD API:** programa que permitirá acceder a diferentes valores de la base de datos mediante operaciones genéricas.
  - **Estudio de la simulación:** se deberán de aprender los principio básicos de la programación y desarrollo con Unity.

- **04/07/2024**

  Se han estudiado los diferentes tipos de sensores que se podrían implementar:

  - **Sensor de movimiento:** dispositivo que permite monitorear la actividad del residente en su hogar, proporcionando información valiosa sobre sus patrones de movimiento y actividad diaria.
  - **Sensores de caídas:** Estos sensores son vitales para detectar inmediatamente cualquier caída, permitiendo el envío rápido de asistencia.
  - **Sensores de temperatura:** estos dispositivos permiten monitorizar las fluctuaciones de temperatura en el hogar, lo que puede ser útil para garantizar un ambiente confortable y seguro. Si la temperatura se desvía de lo normal, se pueden tomar medidas para corregirla rápidamente.
  - **Sensores de apertura:** instalados en puertas y ventanas, estos sensores pueden ayudar a determinar la ubicación del residente de la casa. Además, pueden emitir alertas si la puerta principal se deja abierta, mejorando así la seguridad del hogar.
  - **Sensores de humo:** en caso de incendio, estos sensores pueden alertar rápidamente al residente y a sus contactos de emergencia, así como a los servicios de bomberos.
  - **Sensores de CO2:** estos sensores complementan a los de humo, ayudando a detectar niveles peligrosos de dióxido de carbono en el aire, lo que puede ser especialmente útil en viviendas con sistemas de calefacción a gas.
  - **Sensores de agua:** estos dispositivos pueden detectar inundaciones o fugas de agua, permitiendo una intervención rápida para prevenir daños y controlar el consumo de agua.
  - **Sensores de luz:** nos permitiría monitorizar la iluminación en cada habitación, proporcionando información sobre los patrones de actividad del residente y ayudando a garantizar una iluminación adecuada.
  - **Sensores de calidad de aire:** estos sensores pueden detectar la presencia de contaminantes externos que podrían ser perjudiciales para la salud del residente.
  - **Sensores de presión:** se pueden colocar en diferentes puntos, como la cama, sofá o sillas. Esto permitiría monitorear la presión ejercida sobre estos objetos y obtener más información de patrones de comportamiento.

- **05/07/2023**

  Después del _feedback_ de la tutora, se van a implementar sensores de sonido para obtener más valores para tratar y mostrar datos.

- **31/08/2023**

  En esta parte, se han implementado diferentes objetos que actuan como un sensor de movimiento, donde cuando el objeto (que tiene tag de _player_) se acerca a su radio de control se muestran los datos en pantalla.

  Se ha pensando en implementar las diferentes características a implementar:

  - Detectar varias personas mediante sensores de sonido y movimiento.

  - Poner sensores de movimiento en todas las habitaciones y sensor de apertura para comprobar si sale o entra de esta misma.

  - Que se activen las luces de la vivienda en base al sensor de movimiento, donde la intensidad irá variando en base a la hora del día.

  - Implementar un ciclo día/noche.

  - Implementar diferentes sensores en todas las habitaciones disponibles.

  - Hacer una especie de contador que permita saber el tiempo que permanece el usuario en cada una de las habitaciones.

  - Mandar la información de cada sensor al receptor, normalizarla y almacenarla en la base de datos.

  - Página web para mostrar la información de los datos recopilados por los sensores de la vivienda.

  - Opción de caídas, y así comprobar diferentes sensores aplicados a estos casos.

- **24/20/2023**

  Se han implementado los sensores de 'puertas' que nos indican cuando algún individuo pasa por ella. Estos valores se envían información a una cola de recepción (se ha implementado para n sensores, es decir, que habrá una cola para cada sensor). Actualmente solo tendremos 5 sensores para implementar, de los cuales se ha implementado el de puertas.

  Por otro lado, se han solucionado algunos errores y eliminando diferentes obstaculos que daban errores en colisiones.

  Finalmente, los sensores (el de puertas) envía información únicamente cuando el receptor esta disponible para recibir los datos.

- **25/10/2023**

  Se ha implementado una nueva versión para los sensores de puertas, donde se ha recreado la interacción del usuario con una puerta para cerrar o abrir.

  Gracias a esto se puede saber el estado de las diferentes puertas de la vivienda y calcular una posible localización del usuario.

  Un problema que ha ocasionado esta implementación ha sido la estructuración de la puerta y el cálculo de la nueva posición respecto a la inicial, es decir, tanto cerrar a 0 grados como abrir a 90 grados.

  También se han trabajado varios elementos relacionados con la implementación de un sistema que incluye un sensor de movimiento para as puertas y hacerla interactuable:

  - **Puerta:** se ha diseñado un script que permita al jugador abrir o cerrar la puerta con una animación (duración estimada de 2 segundos), donde tiene un estado de abierta o cerrada.

  - **Receptor:** se ha modificado para que muestre el estado de la puerta en el momento que haya alguna interacción con esta.

  - **Interacción:** se ha añadido un texto que sale en pantalla cuando te acercas a la puerta para interacturas con ella.

  Estos son los problemas que he encontrado durante este proceso:

  - La colocación del pivote correctamente (eje de giro de la puerta) con un _trigger_ para activar diferentes eventos.

  - La modificación para el uso de _Quaternion.Lerp_ en vez de _Math.LerpAngle_ para resolver la animación de la puerta.

  En general, se ha diseñado e implementado un sistema en _Unity_ y _Python_ para detectar y enviar datos de movimiento y estado de la puerta al receptor. Se han resuelto varios problemas como puede ser la animación de la puerta y, finalmente, se ha añadido una indicación **UI** para informar al jugador cuando puede interactuar con la puerta.

- **07/02/2024**

  Se ha implementado una primera versión de la base de datos a implementar, ya que se quiere que el servidor cuando reciba los datos desde el receptor se encargue de almacenar los valores en cada una de las filas en base a la hora que se ha conseguido la información. Esta se irá añadiendo a la base de datos cuando haya algún tipo de modificación en los últimos 5 minutos o cuando un sensor haya enviado un dato (valor actual diferente al anterior principalmente).

  Es decir, el receptor irá obteniendo valores y los irá normalizando porque recibiremos estos valores en formato tipoSensor_lugar_dato_idUsuario_fechaHora y tendremos que enviar estos datos de manera individual en un vector o algo parecido para que el servidor únicamente compruebe si el dato es diferente y/o la fecha es mayor a 5 minutos, entonces se almacenará. De esta manera, cuando queramos llamar para consultar datos y/o gráficas podemos filtrar estos datos en base al filtro de la página web.
  He pensando que en los usuarios podríamos tener una sección de consumo donde habrá una tabla para el consumo de agua, luz y gas, que contendrá los valores que irán registrando los sensores y estarán enlazados con el consumo actual de la empresa suministradora para tener una gráfica y un consumo actual, para poder ver el consumo durante unas fechas, etc. Versión: bd_070224_1.txt/pdf

- **08/02/2024**

  Se ha optimizado la estructura de las tablas:
  Unificación de las tablas de sensores: Las tablas movement_sensor, door_sensors, humity_sensor, temperature_sensor, luminosity_sensor, sound_sensor se han fusionado en una única tabla sensor_readings. Esto reduce la redundancia y facilita las consultas y la inserción de nuevos tipos de sensores en el futuro.

- **15/02/2024**

  Se ha creado el repositorio de GitHub para compartir los avances de código y no perderlos.

  Por otro lado, se ha buscado una forma de implementar una base de datos relacionada con el usuario y los suministros que tenga contratados con diferentes compañias.

  Esta información se representará en base a 5 tablas (150224_v1), donde podremos encontrar diferentes valores para guardar esta información en su
  respectiva tabla. De esta manera conseguiremos en un futuro poder representar gráficamente los valores.
  Segunda versión implementada de la base de datos.
  Se ha creado un código en python que permite modificar las diferentes tablas únicamente con dos valores pasado por parámetro de cada función.
  Se han implementado diferentes funciones para la API de la base de datos

**Avances futuros:**

Implementar código de la API para la base de datos, tanto su creación como modificación. Por otro lado, se quiere subir diferentes imagenes al README.md para mostrar diferentes aspectos de la evolución del proyecto.

**Este proyecto se encuentra en desarrollo y se irán actualizando los avances en este archivo README.md.**
