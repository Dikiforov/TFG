# Vivienda Inteligente - Proyecto en Unity y Arduino

Este proyecto implementa una vivienda de prueba en Unity, ambientada en mi propia vivienda, con una serie de sensores para monitorizar diferentes aspectos del hogar.

## Sensores

- **Movimiento:** Detección de movimiento dentro de la vivienda.
- **Puertas:** Monitorización del estado de las puertas (abiertas/cerradas).
- **Humedad:** Medición del nivel de humedad en el ambiente.
- **Temperatura:** Control de la temperatura ambiente.
- **Luminosidad:** Detección del nivel de luz en la vivienda.
- **Sonido:** Captura de eventos sonoros.
- **Presión atmosférica:** Obtendrá valores para comparar y para su uso médico.
- **Humo, CO2 y calidad de aire:** Valores necesarios para las seguridad y tratado de datos.
- **Inudaciones:** Permitirá saber si se produce una inundación o fuga de agua, lo que nos permitirá controlar su flujo.

## Flujo de datos

1. Los sensores envían información a través de un puerto local del ordenador.
2. Un receptor de datos normaliza la información y la envía al servidor.
3. El servidor almacena los datos en la base de datos mediante una API.
4. Una página web con un backend realiza peticiones a la API para visualizar la información.
5. Los usuarios pueden iniciar sesión y ver diferentes datos del sistema.

## Progresión temporal

---

- **30/06/2023**

  Después de realizar diferentes búsquedas sobre los sensores se ha decidido lo siguiente:

  Utilizar algún tipo de herramienta digital para crear un pequeño circuito y obtener valores (simulación), una de estas herramientas podría ser Unity o Unreal Engine para realizar un modelado de una vivienda junto a sus respectivos sensores.

---

- **03/07/2023**

  - **Busqueda de sensores:** Esto permitirá hacer una simulación de cada uno de estos en base a su precio y características.
  - **Uso de los sensores:** Principalmente servirá para saber como conectarlos y recopilar los datos.
  - **Creación de la base de datos:** Servirá para recopilar en un mismo lugar todos los datos obtenidos por los sensores en unity y, posteriormente, mostrar los resultados en una futura página web.
  - **Programa Sensor-BD:** Programa que irá recopilando los datos de los sensores y normalizandolos.
  - **BD API:** programa que permitirá acceder a diferentes valores de la base de datos mediante operaciones genéricas.
  - **Estudio de la simulación:** Se deberán de aprender los principio básicos de la programación y desarrollo con Unity.

---

- **04/07/2024**

  Se han estudiado los diferentes tipos de sensores que se podrían implementar:

  - **Sensor de movimiento:** Dispositivo que permite monitorear la actividad del residente en su hogar, proporcionando información valiosa sobre sus patrones de movimiento y actividad diaria.
  - **Sensores de caídas:** Estos sensores son vitales para detectar inmediatamente cualquier caída, permitiendo el envío rápido de asistencia.
  - **Sensores de temperatura:** Estos dispositivos permiten monitorizar las fluctuaciones de temperatura en el hogar, lo que puede ser útil para garantizar un ambiente confortable y seguro. Si la temperatura se desvía de lo normal, se pueden tomar medidas para corregirla rápidamente.
  - **Sensores de apertura:** Instalados en puertas y ventanas, estos sensores pueden ayudar a determinar la ubicación del residente de la casa. Además, pueden emitir alertas si la puerta principal se deja abierta, mejorando así la seguridad del hogar.
  - **Sensores de humo:** En caso de incendio, estos sensores pueden alertar rápidamente al residente y a sus contactos de emergencia, así como a los servicios de bomberos.
  - **Sensores de CO2:** Estos sensores complementan a los de humo, ayudando a detectar niveles peligrosos de dióxido de carbono en el aire, lo que puede ser especialmente útil en viviendas con sistemas de calefacción a gas.
  - **Sensores de agua:** Estos dispositivos pueden detectar inundaciones o fugas de agua, permitiendo una intervención rápida para prevenir daños y controlar el consumo de agua.
  - **Sensores de luz:** Nos permitiría monitorizar la iluminación en cada habitación, proporcionando información sobre los patrones de actividad del residente y ayudando a garantizar una iluminación adecuada.
  - **Sensores de calidad de aire:** Estos sensores pueden detectar la presencia de contaminantes externos que podrían ser perjudiciales para la salud del residente.
  - **Sensores de presión:** Se pueden colocar en diferentes puntos, como la cama, sofá o sillas. Esto permitiría monitorear la presión ejercida sobre estos objetos y obtener más información de patrones de comportamiento.

---

- **05/07/2023**

  Después del _feedback_ de la tutora, se van a implementar sensores de sonido para obtener más valores para tratar y mostrar datos.

---

- **29/08/2023**

  Se ha encontrado la herramienta _Sweet Home 3D_ para recrear la vivienda y, después, exportarla a un **.obj** utilizable en _unity_.

  A continuación se muestra el domicilio modelado en el programa para su apreciación, para esto se han obtenido medida reales de la vivienda y representado:

  ![Plano de la vivienda](/images/Plano_Vivienda.png "Plano de la vivienda")

  Por otro lado, después de realizar este plano de la vivienda, se muestran dos imagenes don diferentes vistas de la vivienda desde una vista exterior superior:

  ![Vista 1](/images/Vista1_Vivienda.png "Vista 1")

  ![Vista 2](/images/Vista2_Vivienda.png "Vista 2")

  Como se puede apreciar, es una vivienda que es parecida a la mayoría, por tanto nos permitirá realizar un estudio que puede resultar interensate en base a la persona que viva en el domicilio. Estos estudios pueden ser el de comparar entre si son pareja, conviven de manera individual o entre hombres y mujeres.

---

- **30/08/2023**

  Se han incorporado los ficheros de código fuente para mover al usuario con las teclas de control _wasd_.

---

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

  ![Primera versión sensor de puertas cuando se pasa de la terraza al salón](/images/primera_idea_sensor_puerta.png "primera idea sensor puerta")

---

- **24/10/2023**

  Se han implementado los sensores de 'puertas' que nos indican cuando algún individuo pasa por ella. Estos valores se envían información a una cola de recepción (se ha implementado para n sensores, es decir, que habrá una cola para cada sensor). Actualmente solo tendremos 5 sensores para implementar, de los cuales se ha implementado el de puertas.

  Por otro lado, se han solucionado algunos errores y eliminando diferentes obstaculos que daban errores en colisiones.

  Finalmente, los sensores (el de puertas) envía información únicamente cuando el receptor esta disponible para recibir los datos.

  A continuación se puede observar cuando el usuario no esta en movimiento y pasa a diferentes estancias:

  ![Usuario sin movimiento en el salón](/images/version1_sensorMovimientoPuertas_nada.png "Usuario sin movimiento en el salón")

  ![Usuario con movimiento en el salón](/images/version1_sensorMovimientoPuertas_salon.png "Usuario con movimiento en el salón")

  ![Usuario en el pasillo después de cruzar el salón](/images/version1_sensorMovimientoPuertas_salonPasillo.png "Usuario en el pasillo después de cruzar el salón")

  Finalmente, esta es la información que recibe el receptor de datos, que deberá de normalizar para enviar al servidor y que se almacene en la base de datos:

  ![Información respecto a lo que recibe el receptor del sensor](/images/version2_sensorPuertasReceptor_recibidorCocina.png "Información respecto a lo que recibe el receptor del sensor")

  ![Información respecto a lo que recibe el receptor del sensor](/images/version2_sensorPuertasReceptor_salonRecibidor.png "Información respecto a lo que recibe el receptor del sensor")

---

- **25/10/2023**

  Se ha implementado una nueva versión para los sensores de puertas, donde se ha recreado la interacción del usuario con una puerta para cerrar o abrir.

  Gracias a esto se puede saber el estado de las diferentes puertas de la vivienda y calcular una posible localización del usuario.

  Un problema que ha ocasionado esta implementación ha sido la estructuración de la puerta y el cálculo de la nueva posición respecto a la inicial, es decir, tanto cerrar a 0 grados como abrir a 90 grados.

  También se han trabajado varios elementos relacionados con la implementación de un sistema que incluye un sensor de movimiento para as puertas y hacerla interactuable:

  - **Puerta:** Se ha diseñado un script que permita al jugador abrir o cerrar la puerta con una animación (duración estimada de 2 segundos), donde tiene un estado de abierta o cerrada.

  - **Receptor:** Se ha modificado para que muestre el estado de la puerta en el momento que haya alguna interacción con esta.

  - **Interacción:** Se ha añadido un texto que sale en pantalla cuando te acercas a la puerta para interacturas con ella.

  Estos son los problemas que he encontrado durante este proceso:

  - La colocación del pivote correctamente (eje de giro de la puerta) con un _trigger_ para activar diferentes eventos.

  - La modificación para el uso de _Quaternion.Lerp_ en vez de _Math.LerpAngle_ para resolver la animación de la puerta.

  En general, se ha diseñado e implementado un sistema en _Unity_ y _Python_ para detectar y enviar datos de movimiento y estado de la puerta al receptor. Se han resuelto varios problemas como puede ser la animación de la puerta y, finalmente, se ha añadido una indicación **UI** para informar al jugador cuando puede interactuar con la puerta.

  **FOTO CON LA INTERACCIÓN**

---

- **07/02/2024**

  Se ha implementado una primera versión de la base de datos a implementar, ya que se quiere que el servidor cuando reciba los datos desde el receptor se encargue de almacenar los valores en cada una de las filas en base a la hora que se ha conseguido la información. Esta se irá añadiendo a la base de datos cuando haya algún tipo de modificación en los últimos 5 minutos o cuando un sensor haya enviado un dato (valor actual diferente al anterior principalmente).

  Es decir, el receptor irá obteniendo valores y los irá normalizando porque recibiremos estos valores en formato tipoSensor_lugar_dato_idUsuario_fechaHora y tendremos que enviar estos datos de manera individual en un vector o algo parecido para que el servidor únicamente compruebe si el dato es diferente y/o la fecha es mayor a 5 minutos, entonces se almacenará. De esta manera, cuando queramos llamar para consultar datos y/o gráficas podemos filtrar estos datos en base al filtro de la página web.
  He pensando que en los usuarios podríamos tener una sección de consumo donde habrá una tabla para el consumo de agua, luz y gas, que contendrá los valores que irán registrando los sensores y estarán enlazados con el consumo actual de la empresa suministradora para tener una gráfica y un consumo actual, para poder ver el consumo durante unas fechas, etc.

  A continuación se muestra como sería el diagrama de la base de datos en su primera versión:

  ![Base de datos en su primera versión](/images/baseDeDatos_primeraVersion.png "Base de datos en su primera versión")

---

- **08/02/2024**

  Se ha optimizado la estructura de las tablas:
  Unificación de las tablas de sensores: Las tablas movement_sensor, door_sensors, humity_sensor, temperature_sensor, luminosity_sensor, sound_sensor se han fusionado en una única tabla sensor_readings. Esto reduce la redundancia y facilita las consultas y la inserción de nuevos tipos de sensores en el futuro.

---

- **15/02/2024**

  Se ha creado el repositorio de GitHub para compartir los avances de código y no perderlos.

  Por otro lado, se ha buscado una forma de implementar una base de datos relacionada con el usuario y los suministros que tenga contratados con diferentes compañias.

  Esta información se representará en base a 5 tablas, donde podremos encontrar diferentes valores para guardar esta información en su
  respectiva tabla. De esta manera conseguiremos en un futuro poder representar gráficamente los valores.
  Segunda versión implementada de la base de datos.
  Se ha creado un código en python que permite modificar las diferentes tablas únicamente con dos valores pasado por parámetro de cada función.
  Se han implementado diferentes funciones para la API de la base de datos

  A continuación se muestra la 2a versión de la base de datos diseñada:

  ![Base de datos en su segunda versión](/images/baseDeDatos_2aVersion.png "Base de datos en su segunda versión")

  Se puede apreciar como se han unificado la tabla de sensores, de tal manera que en _sensor_reading.sensor_type_ nos encotraremos con estos posibles sensores:

  - movimiento
  - puertas
  - humedad
  - temperatura
  - luminosidad
  - sonido

  Por otro lado, en la tabla de _supplies_ nos encontramos con diferentes valores que irán entre el nombre del suministro (gas, luz o agua), la unidad con la que se mide este suministro, el tipo de contrato con el suministros (fijo o variable).

  También se ha añadido el consumo general del suministro en base al mes actual, cosa que se irá actualizando en base a la factura y la compañía contratada.

  Finalmente, para obtener más datos, se ha implementado una tablas extras en las que podemos tener valores por diferentes tiempos, como puede ser mensual, bimensual, trimestras, semestral o anual, junto al consumo por horas que también se verá reflejado.

  Respecto a _unity_, se han incorporado los diferentes ficheros de código fuente que se han elaborado para probar las diferentes funcionalidades, como puede ser el movimiento del jugador (día **30/08/2023**) o interacción con diferentes sensores (día **25/10/2023**).

  Se ha decidido que no se puede coger sensores ya implementados en el mercado ya que el coste de la instalación en un domicilio particular sería muy elevado, por tanto creo que la mejor opción es la compra se sensores para placas, como _arduino_ o _raspberry_, para conectarlos los diferentes sensores compatibles y, de esta manera, tener un coste menor.

  Se ha pensando en que cada habitación tenga una placa de tamaño reducido (serie nano) para que tenga una ocupación de espacio menor, donde cada sensor enviará información a este dispositivo. Para enviar información a la placa base general, se ha pensado en que cada placa tenga un módulo de comunicación que permita transmitir los datos tratados a una placa general que permita observar los diferentes valores de cada estancia (placa base tipo GIGA display).

  Un enlace, porque se ha optado ahora mismo el trabajar con arduino, para observar una gran variedad de sensores y su funcionalidad: https://www.electrogeekshop.com/tutoriales-de-sensores-con-arduino.

  Listado de placas: https://www.arduino.cc/en/hardware.

  Listado de sensores aptos para su uso con arduino:

  - **Luminosidad:** Luxometro BH1750, para comprobar la cantidad de luz en cada una de las estancias.
  - **Temperatura y humedad:** DHT22, para obtener los valores de temperatura y humedad. Para la temperatura también se puede utilizar el LM35DZ.
  - **Sonido:** KY-037, sensor para comprobar y discrimar valores. Así se puede obtener una medición de si se reciben visitas o no.
  - **Movimiento:** HC-SR05 y HC-SR501, mediante ultrasonidos podemos situar al usuario y situarlo con más precisión gracias al sensor de movimiento.
  - **Presión:** TTP223B permitiría saber si el usuario se ha sentando en algún lado o no.
  - **Presión atmosférica:** BMP280
  - **Humo, CO2 y calidad de aire:** CCS811, permite recopilar todos estos datos.
  - **Inundaciones:** Sensor para comprobar si se produce algún tipo de inundación en alguna de las estancias.
  - **Gas:** MQ-9 para comprobar si hay fugas de gas en el domicilio.
  - **Caudalimetro:** Servirá para medir la cantidad de agua gastada en toda la estancia.
  - **Comunicación:** RF 433 Mhz, para poder comunicarnos entre las diferentes placas mediante radiofrecuencia. Otra opción es un módulo Wi-Fi(ESP8266) o Bluetooth(HC05) para la transmisión de datos.

  Coste global:

  - **Placa base para los sensores:** 20€
  - **Placa base general:** 60€
  - **Caudalímetro:** 6.5€

  Cada uno de los sensores se compartirá para cada placa por igual, es decir, este es el precio por 1 placa y no en total:

  - **Sensor de luminosidad:** 0.85€
  - **Sensor de temperatura y humedad:** 2.40€
  - **Sensor de sonido:** 2€
  - **Sensor de movimiento:** 4.31€ + 2.24€ = 6.55 €
  - **Sensor de presión:** 1.57€
  - **Sensor de presión atmosférica:** 2.40€
  - **Sensor de humo, CO2 y calidad de aire:** 5€
  - **Sensor de inundaciones:** 1.49€
  - **Sensor de fuga de gas:** 2.89€
  - **Sensor de comunicación:** 8€

  Estos son los precios de cada elemento. Por tanto, si tenemos en cuenta una media de 10 estancias por vivienda debemos de considerar lo siguiente:

  **PONER UNA MEJOR FÓRMULA**

  n_placas \* (0.85 + 2.40 + 2.00 + 6.55 + 1.57 + 2.40 + 5.00 + 1.49 + 2.89 + 8) = n_placas\*33.15 + 60.00 = 10\*33.15 + 60 = 391.50€ + 6.50\*3 (lavabo, cocina y galeria) = 411€.

  Podemos considerar que la instalación de 10 placas con su respectivo controlador principal ocasionarian unos gastos aproximados de 450€ sin tener en cuenta el cableado, mano de obra ni posibles complicaciones.

---

- **16/02/2024**

  Se ha buscado alguna herramienta con la que podamos conectar todos los elementos necesarios por cada placa y sus sensores, es decir, la instalación que tendría cada estancia sin contar lugares en los que se deba de instalar caudalímetros y sensores de inundación. La herramienta encontrada ha sido _circuito.io_, que nos permite seleccionar la placa base deseada y los elementos a conectar, cosa que realiza de manera automática y que, además, nos proporciona un código fuente base para el funcionamiento completo de todo el conjunto.

  ![Primera versión de la placa base con sus componentes](/images/Placa_arduino.png "Primera versión de la placa con sus componentes")

  Como se puede apreciar en la imagen anterior, disponemos de una placa base que tiene un módulo de comunicación Wi-Fi para la transmisión de los datos y que, además, cuenta con un total de 14 componentes (incluidos los de alimentación y excluido el módulo de comunicación).

  Gracias a la herramienta de _circuito.io_ podemos observar como sería la conexión de cada uno de estos elementos a la placa y así podemos calcular el tamaño que ocuparía cada una de las cajas con este diseño. Hay que recalcar que este diseño esta realizado de cara a una estancia general, no esta contemplado estancias con instalación de corriente de agua. Ya que estas deberán de incluir sensores para posibles inundaciones y caudalímetros para el control de flujo de agua, esto permitirá tener un control más exhaustivo y, en todo caso, poder cerrar la corriente si es necesario.

  A continuación se muestra el diseño que seguiriamos para la cocina, ya que contamos que el grifo de esta tiene las entradas de agua caliente y fría, por tanto se necesitará un total de 2 controladores de agua, una para cada.

  ![Primera versión de la placa base con sus componentes en la cocina](/images/Placa_arduino_cocina.png "Primera versión de la placa con sus componentes en la cocina")

  Por otro lado, se expose el diseño que se seguiría para la instalación completa y control en una baño típico (con una bañera o una ducha), por tanto tendremos para cada salida de agua 2 controladores y para el sanitario otro controlador. Por tanto, tendríamos 2 controladores para el frigo, 1 para el sanitario y otro para la ducha o bañera.

  ![Primera versión de la placa base con sus componentes en el baño](/images/Placa_arduino_bathroom.png "Primera versión de la placa con sus componentes en el baño")

  Hay que recalcar que estos diseños estan en su primera versión y que esta sería la forma más óptima de conectar todo, pero esto no se dará ya que la caja que contendrá la placa, los diferentes sensores y los controladores de flujo se colocarán en lugares estratégicos para impedir una manipulación indebida de estos, así como mejorar la seguridad y el uso correcto.

  Finalmente, respecto a los diseños, la herramienta _circuito.io_ también nos realiza un posible cálculo del coste de los elementos instalados, hay que aclarar que estos precios son aproximados en base a la información que contiene la página web y no pueden ser del todo correctos.

  ***

  A continuación se muestra un listado de todos los elementos utilizados en los diagramas anteriores junto a su precio, por otro lado, se muestra una tabla con los precios y cantidad de elementos que tendremos en cada estancia y, finalmente, un cálculo de todo el coste general de los componentes:

---

- **19/02/2024**

  - **Precio componentes habitación:**

    | Sensor                                                        | Precio  | Cantidad |   Total |
    | :------------------------------------------------------------ | :-----: | -------: | ------: |
    | Arduino Pro Mini 328 - 5V/16MHz                               | 9,95 €  |        1 |  9,95 € |
    | PIR (motion) sensor                                           | 9,95 €  |        1 |  9,95 € |
    | SparkFun Sound Detector                                       | 10,95 € |        1 | 10,95 € |
    | SparkFun BME280 - Atmospheric Sensor Breakout                 | 19,95 € |        1 | 19,95 € |
    | Wall Adapter Power Supply - 12VDC 2A                          | 15,77 € |        1 | 15,77 € |
    | N-Channel MOFSET 60V 30A                                      | 1,46 €  |        2 |  2,92 € |
    | 10K Ohms Resistor                                             | 0,10 €  |        1 |  0,10 € |
    | Water Level Sensor Module                                     | 0,78 €  |        1 |  0,78 € |
    | Carbon Monoxide and Combustible Gas Sensor - MQ-9             | 1,60 €  |        1 |  1,60 € |
    | Adafruit TSL2591 High Dynamic Range Digital Light Sensor      | 6,95 €  |        1 |  6,95 € |
    | Adafruit SGP30 Air Quality Sensor Breakout - VOC and eCO2     | 19,95 € |        1 | 19,95 € |
    | ESP8266-01 - Wifi Module                                      | 6,95 €  |        1 |  6,95 € |
    | Logic Level Converter - Bi-Directional                        | 2,95 €  |        1 |  2,95 € |
    | DHT2/11 Humity and Temperature Sensor                         | 9,95 €  |        1 |  9,95 € |
    | Female DC Power adapater - 2.1mm jack to screw terminal block | 2,00 €  |        1 |  2,00 € |
    | Voltage Regulator 3.3V                                        | 0,55 €  |        1 |  0,55 € |
    | Capacitor Ceramic 100nF                                       | 0,64 €  |        1 |  0,64 € |
    | Electrolytic Decoupling Capacitors - 100uF/25V                | 0,27 €  |        1 |  0,27 € |
    | USB Mini-B Cable with FTDI                                    | 3,11 €  |        1 |  3,11 € |
    | BreadBoard                                                    | 8,25 €  |        2 | 16,50 € |
    | Jumper Wires Pack - M/M                                       | 1,95 €  |        3 |  5,85 € |
    | Jumper Wires Pack - M/F                                       | 1,95 €  |        1 |  1,95 € |
    | Male Headers Pack - Break-Away                                | 0,66 €  |        2 |  1,32 € |
    |                                                               |         |    Total | 150,91€ |

  - **Precio componentes cocina:**

    | Sensor                                                        | Precio  | Cantidad |   Total |
    | :------------------------------------------------------------ | :-----: | -------: | ------: |
    | Arduino Pro Mini 328 - 5V/16MHz                               | 9,95 €  |        1 |  9,95 € |
    | 12 V Solenoid Valve - 3/4"                                    | 7,95 €  |        2 | 15,90 € |
    | Diode Rectifier - 1A 50V                                      | 0,11 €  |        2 |  0,22 € |
    | PIR (motion) sensor                                           | 9,95 €  |        1 |  9,95 € |
    | SparkFun Sound Detector                                       | 10,95 € |        1 | 10,95 € |
    | SparkFun BME280 - Atmospheric Sensor Breakout                 | 19,95 € |        1 | 19,95 € |
    | Wall Adapter Power Supply - 12VDC 2A                          | 15,77 € |        1 | 15,77 € |
    | N-Channel MOFSET 60V 30A                                      | 1,46 €  |        2 |  2,92 € |
    | 10K Ohms Resistor                                             | 0,10 €  |        3 |  0,30 € |
    | Water Level Sensor Module                                     | 0,78 €  |        1 |  0,78 € |
    | Carbon Monoxide and Combustible Gas Sensor - MQ-9             | 1,60 €  |        1 |  1,60 € |
    | Adafruit TSL2591 High Dynamic Range Digital Light Sensor      | 6,95 €  |        1 |  6,95 € |
    | Adafruit SGP30 Air Quality Sensor Breakout - VOC and eCO2     | 19,95 € |        1 | 19,95 € |
    | ESP8266-01 - Wifi Module                                      | 6,95 €  |        1 |  6,95 € |
    | Logic Level Converter - Bi-Directional                        | 2,95 €  |        1 |  2,95 € |
    | DHT2/11 Humity and Temperature Sensor                         | 9,95 €  |        1 |  9,95 € |
    | Female DC Power adapater - 2.1mm jack to screw terminal block | 2,00 €  |        1 |  2,00 € |
    | Voltage Regulator 3.3V                                        | 0,55 €  |        1 |  0,55 € |
    | Capacitor Ceramic 100nF                                       | 0,64 €  |        1 |  0,64 € |
    | Electrolytic Decoupling Capacitors - 100uF/25V                | 0,27 €  |        1 |  0,27 € |
    | USB Mini-B Cable with FTDI                                    | 3,11 €  |        1 |  3,11 € |
    | BreadBoard                                                    | 8,25 €  |        2 | 16,50 € |
    | Jumper Wires Pack - M/M                                       | 1,95 €  |        4 |  7,80 € |
    | Jumper Wires Pack - M/F                                       | 1,95 €  |        1 |  1,95 € |
    | Male Headers Pack - Break-Away                                | 0,66 €  |        2 |  1,32 € |
    |                                                               |         |    Total | 169,18€ |

  - **Precio componentes baño:**

    | Sensor                                                        | Precio  | Cantidad |     Total |
    | :------------------------------------------------------------ | :-----: | -------: | --------: |
    | Arduino Pro Mini 328 - 5V/16MHz                               | 9,95 €  |        1 |    9,95 € |
    | 12 V Solenoid Valve - 3/4"                                    | 7,95 €  |        5 | 39,75 € € |
    | Diode Rectifier - 1A 50V                                      | 0,11 €  |        5 |    0,55 € |
    | PIR (motion) sensor                                           | 9,95 €  |        1 |    9,95 € |
    | SparkFun Sound Detector                                       | 10,95 € |        1 |   10,95 € |
    | SparkFun BME280 - Atmospheric Sensor Breakout                 | 19,95 € |        1 |   19,95 € |
    | Wall Adapter Power Supply - 12VDC 2A                          | 15,77 € |        1 |   15,77 € |
    | N-Channel MOFSET 60V 30A                                      | 1,46 €  |        5 |    7,30 € |
    | 10K Ohms Resistor                                             | 0,10 €  |        6 |    0,60 € |
    | Water Level Sensor Module                                     | 0,78 €  |        1 |    0,78 € |
    | Carbon Monoxide and Combustible Gas Sensor - MQ-9             | 1,60 €  |        1 |    1,60 € |
    | Adafruit TSL2591 High Dynamic Range Digital Light Sensor      | 6,95 €  |        1 |    6,95 € |
    | Adafruit SGP30 Air Quality Sensor Breakout - VOC and eCO2     | 19,95 € |        1 |   19,95 € |
    | ESP8266-01 - Wifi Module                                      | 6,95 €  |        1 |    6,95 € |
    | Logic Level Converter - Bi-Directional                        | 2,95 €  |        1 |    2,95 € |
    | DHT2/11 Humity and Temperature Sensor                         | 9,95 €  |        1 |    9,95 € |
    | Female DC Power adapater - 2.1mm jack to screw terminal block | 2,00 €  |        1 |    2,00 € |
    | Voltage Regulator 3.3V                                        | 0,55 €  |        1 |    0,55 € |
    | Capacitor Ceramic 100nF                                       | 0,64 €  |        1 |    0,64 € |
    | Electrolytic Decoupling Capacitors - 100uF/25V                | 0,27 €  |        1 |    0,27 € |
    | USB Mini-B Cable with FTDI                                    | 3,11 €  |        1 |    3,11 € |
    | BreadBoard                                                    | 8,25 €  |        2 |   16,50 € |
    | Jumper Wires Pack - M/M                                       | 1,95 €  |        5 |    9,75 € |
    | Jumper Wires Pack - M/F                                       | 1,95 €  |        1 |    1,95 € |
    | Male Headers Pack - Break-Away                                | 0,66 €  |        2 |    1,32 € |
    |                                                               |         |    Total |  199,99 € |

    Después de tener el coste de cada una de las cajas que contendran los diferentes dispositivos para la obtención de datos, tenemos este presupuesto mínimo para la instalación de los componentes:

    | Habitaciones |  Baños   |  Cocinas | Pasillos | Coste total |
    | :----------- | :------: | -------: | -------: | ----------: |
    | 5            |    1     |        1 |        2 |
    | 754,55 €     | 199,99 € | 169,18 € |   301,81 |  1.425,54 € |

    Por tanto, después de realizar este estudio, podemos apreciar como sería el coste mínimo, ya que debemos de tener en cuenta que este presupuesto es única y exclusivamente para la placa y los respectivos sensores. Por lo que no esta incluida la mano de obra, el servidor ni los programas específicos a implementar.

    ***

    Actualmente, se puede apreciar como sería el diagrama de flujo de los datos:

    ![Diagrama de flujo de los datos](/images/Diagrama_flujo_datos.png "Diagrama de flujo de los datos")

    - Por un lado tenemos los sensores, que van enviando información a la placa específica a la que estan conectados cada cierto tiempo.
    - Una vez la placa específica ha recibido los datos, lo que hará será compararlos con los que tenía anteriormente. Si estos valores son iguales, los descartará a no ser que hayan pasado unos 5 minutos (esto puede modificarse). Esto se ha decidido así ya que permite no saturar el sistema de datos y, de esta manera, tener un mejor flujo de datos. Por otro lado, a la hora de enviarlos a la placa general, esta placa se encargará de encriptarlos para evitar posibles corrupciones de datos.
    - Cuando estos datos encriptados son enviados y recibidos por la placa general, esta los desencriptará y enviará al servidor (estará alojado en una dirección específica y podrá modificar esta base de datos con una clave que se le proporcionará a la hora de realizar la instalación).
    - Los datos de la base de datos estarán organizados en base al diseño mostrado anteriormente.
    - Como se ha propuesto también crear una página web en la que poder consultar estos datos obtenidos, se podrá acceder mediante unas credenciales proporcionadas y que nos permitirá realizar diferentes consultas:

      - Mirar el valor de los sensores.
      - Observar el domicilio completo con sus diferentes datos en cada estancia.
      - Poder filtrar en base a lo que se desee, como puede ser fechas u horas específicas, mostrar únicamente 'x' sensores o diferentes opciones (aún sin implementar ni pensadas cómo).
      - Consultar el valor de cada suministro contratado y comparalo en base a los valores obtenidos por los diferentes sensores.

    (En este diseño se han tenido únicamente en cuenta sensores y no lectores de consumo energético como enchufes inteligentes, bombillas inteligenetes o cualquier herramienta IOT).

---

- **20/02/2024**

  Se ha buscando una forma de implementar un ciclo de día y noche, en base a este y los valores medios de Tarragona, se hará una simulación de la temperatura ambiente del domicilio. Esto se realizará mdiente una variables globales que permitirán modificar las variables de los sensores de humedad y temperatura en base a la estación seleccionada, ya que esta estación permitirá modificar el tiempo del ciclo de día y noche (más horas de luz o menos), las temperaturas (dependiendo de la estación estaremos en un baremos diferentes de temperaturas) y la humedad (ya que será un sistema parecido al de la temperatura).

  El video que se ha usado como guía es el siguiente: _https://www.youtube.com/watch?v=FIjCwUj3As4_

  También se han añadido el resto de puertas faltantes y también implementada su lógica a falta de algunos retoques.

  ![Piso con puertas](/images/Domicilio_puertasImplementadas.png "Piso con las puertas")

  Por otro lado, se ha implementado el _script_ 'DayNightCicle' para el ciclo de día y noche.

---

- **02/03/2024**

  Se ha implementado una nueva funcionalidad de día y noche, donde la _directional light_ o "sol" se desplaza en alrededor del domicilio, para esto se ha realizado lo siguiente:

  - Crear un material.
  - Con el material seleccionado, convertirlo en _skybox material_.
  - Configurar el material creado para que genere sombras de manera procedural.
  - Configuraciones varias para colores, tonalidades, etc.
  - Crear un script que permita, en base a la velocidad del día y de la hora, crear un ciclo de día y noche.

  Por otro lado, se han rehecho toda la estructura del domicilio ya que de la anterior manera no se podía configurar diferentes opciones en base a las colisiones ni texturas.

  ![Nuevo Piso](/images/NuevoDise.png "Piso creado con diferentes cubos")

  También, cuando se ha acabado de implementar el ciclo de día y noche, podemos apreciar en las siguientes imagenes como cambia las tonalidades de los objetos:

  ![Horario nocturno](/images/Domicilio_HorarioNocturno.png "Domicilio con horario nocturno")

  ![Horario diurno](/images/Domicilio_HorarioDiurno.png "Domicilio con horario diurno")

  Como se puede apreciar en las imagenes, tanto las texturas del domicilio como las del entorno cambián, ya que estas pruebas son a las 00:00 y a las 08:00. Esto nos permitirá en un futuro, en base a una estación que selecionemos, hacer una pequeña simulación de temperatura y humedad en el domicilio para registrar datos más variados y probar diferentes sensores.

  Esto se realizará de la siguiente manera:

  - Obtener una media de temperatura y humedad de la ciudad de Tarragona en las diferentes estanciones.
  - Con estos valores, se creará una pequeña condición global que permitirá aleatorizar los diferentes valores obtenido para que haya variación en el entorno.

  Con estos cálculos, en base a la estancia deseada y posición del usuario, aumentar un poco la temperatura, disminuirla, etc. Esto también se verá afectado en base a la duración, acción o diferentes factores que se puedan realizar en la estancia.

  Para realizar este tipo de movimientos, se ha pensando en implementar un pequeño circuito que vaya siguiente el usuario de manera constante para generar valores y así evitar obtenerlos de manera manual. Esto tendrá una lógica similar a la de la temperatura y humedad, ya que se hará mediante unos parámetros que se irán generando por cada movimiento del usuario. De esta manerá también podremos simular diferentes usuarios a la vez.

  Finalmente, se ha implementado una cámara (vista de planta) para que, a la hora de simular todo, podamos ver las diferentes rutinas de cada usuario en cuestión.

---

- **13/03/2024**

  Se ha generado una pequeña prueba en _unity_ para el cálculo y simulación de la temperatura en base a la hora y valores máximos/mínimos de temperatura. De esta manera podemos obtener valores para comprobar diferentes parámetros, como puede ser la temperatura interior del domicilio en base a donde se encuentre el usuario.

  Por otro lado, actualmente tenemos valores que son contantes mediante un cálculo donde lo que se realiza es dividir la temperatura origen y destino entre el intervalo de horas deseado para que aumente o decremente la temperatura, de esta manera, como se ha comentado anteriormente, tenemos valores constantes y no muy realistas.

  ![Gráfico constantes](/images/GráficoConstantes.png "Gráfico con valores incrementales constantes")

  Como se puede observar en este gráfico, las temperaturas van modificandose de manera constante en base a la hora que queramos y el intervalo deseado.

  ![Gráfico curva temperatura](/images/GráficaGeneralTemperatura.png "Gráfico curva temperatura objetivo")

  En cambio, en el gráfico anterior podemos observar como funciona la curva de temperatura. Si nos fijamos, apreciamos como sería este último gráfico algo más realista en base a las temperaturas que podamos tener, para esto se han relizado diferentes pruebas en la plataforma _www.geogebra.org_, donde podemos apreciar que en base a la pendiente y unas serie de valores que actuarán como principalmente las temperaturas máximas y mínimas deseadas, hay una mejor relación y movimiento entre estas a partir de la hora del día actual.

  Para la implementació de esta curva, se ha encontrado un objeto en unity, denominado _AnimationCurve_, que permite realizar este tipo de cálculos. Por otro lado, también es posible realizarlo con operaciones matemáticas.

---

- **14/03/2024**

  Una vez estudiada más a fondo la función implementada en unity de _Mathf.Lerp()_ se ha podido implementar una curva de temperatura más suave en base a 3 factores:

  - **Estación seleccionada:** en base a la estación que se haya escogido en la jerarquía, se aplicarán en un inicio diferentes valores para las máximas y mínimas relacionadas con la temperatura. (Código Start)

  - **Hora actual:** como dependiendo de la hora las temperaturas debes ser más altas/bajas, se relaciona en base a tres intervalos que comprenden desde las 00:00-03:59 (incrementamos un grado), 04:00-07:59 (incrementamos otro grado), 08:00-11:59 (desde nuestra última temperatura registrada hasta el máximo), 12:00-15:59 (temperaturas máximas) y 16:00-23:59 (decremento de temperatura máxima a mínima).

  - **Nuevo cálculo de máximas y mínimas:** inicialmente seleccionamos una estación, que contendrá un valor máximo y mínimo para las temperaturas, pero como cada día puede ser diferentes, se ha optado por aleatorizar cada 24 horas internas estos valores que nos sirven de abanico. Esto nos permite que cada día haya unos registros diferentes con la posibilidad de que algún día tenga la misma información. Esto es únicamente para que, a la hora de generar gráficos u otras lecturas de datos, haya una distribución más lógica.

  Para la implementación de estas curvas, se ha utilizado la función mencionada anteriormente (_Mathf.Lerp()_) que nos permite realizar una interpolación lineal (cálculo para el valor intermedio como una combinación lineal de dos valores conocidos), donde en este caso nos aplicarlo a la temperatura, ya que nos permite crear transiciones o curvas suaves entre diferentes puntos de temperaturas a lo largo del día.

  (Anexo con más información)

  Como parámetros nos encontramos con:

  - _a:_ valor inicial de la interpolación.
  - _b:_ valor final de la interpolación.
  - _t:_: valor que controla la posición dentro del intervalo de interpolación.

  Un ejemplo, aplicando valores del código, sería:

  (insertar fórmula)
  a = TempMinima (7.5 ºC)
  b = TempMinima + 1 ºC (8.5 ºC)
  t = (Hora - 0) / 4 (desde las 00:00 hasta las 03:59, ya que son 4 horas como tal y queremos que haya un incremento equitativo por horas).

  De esta manera, para las 02:00 de la madrugada, tendremos una temperatura de 8 grados, ya que la _t_ nos daría 0.5 y ese sería nuestro incremento por horas.

  Enlaces:

  - https://docs.unity3d.com/ScriptReference/Mathf.Lerp.html
  - https://www.youtube.com/watch?app=desktop&v=RNccTrsgO9g
  - https://www.youtube.com/watch?v=q-1bFcMdUcg

  Una vez aplicadas las temperaturas, estas se tendrán que enviar al servidor que acturará como receptor de datos, que los almacenará en una base de datos. Para evitar un envío masivo de datos, se programará una forma en la que cada se vayan comparando valores cada minuto, si estos cambian se enviarán todos los datos al servidor, pero si estos no cambian, se enviarán de manera automática cada 5 minutos, de esta manera evitamos saturar el servidor con tantas llamadas de almacenamiento.
  Por otro lado, esta es la implementación de la temperatura ambiente, es decir, de la calle. En caso de la viviendo, se aumentará la temperatura una cantidad constante (a excepción de que sea verano), esto es debido a que dentro de una vivienda la temperatura humana y la calidez de esta permite una mayor temperatura en el entorno. Esta temperatura de las estancias de la vivienda también irá relacioanda con la posición del usuario y la hora, ya que cuando vaya a preparar la comida, la cocina aumentará de temperatura en base al individuo y la propia cocción de los alimentos. En cambio, para el tema de la humedad, también influirá la temperatura en esta, ya que cuanto más haya puede haber más calor o más frio, cosa que se debe de estudiar aún el método de como realizarlo.

  En fin, la implementación de la temperatura ambiente esta implementada pero hay que aumentar esta misma dentro del domicilio, teniendo en cuenta la posición del usuario, la humedad y las horas del día. Por otro lado, la implementación de la humedad será pareciada a la de la temperatura, ya que será comprobar valores por horas.

---

- **26/03/2024**

  Se ha añadido mensajes de _textMesh Pro_ en la parte derecha de la simulación para comprobar diferentes datos de los sensores, como puede ser la temperatura, humedad, presión, movimiento o estado de las puertas. Para esto, se ha creado un objeto _canva_ que almacenará todos los _textMesh Pro_ con los datos de los sensores. Esto implica que en todos los scripts que teníamos creados deberán de incluir un nuevo objeto público que será el mensaje mostrado.
  _https://www.youtube.com/watch?v=bR0clpZvjXo_

  ![Datos en pantalla](/images/InfoSensores.png "Datos en pantalla")

  Como se puede apreciar, aparecen una gran cantidad de informació y, aun así, no esta completa, ya que únicamente se muestra la información de los sensores de temperatura, humedad y presión genéricos provocados por el ambiente. El objetivo es tener por cada habitación, en el momento que entramos, un cambio de datos en pantalla, lo que nos permitirá saber la información de cada habitación en concreto. Aunque queremos que se pueda cambiar con un botón, aunque esto será sencillo, ya que la idea es que durante la ejecución de la simulación tengamos en cada una de las estancias un cuadro de texto con información específica y general, es decir, que durante la rutina _update_ de la simulación y, si pulsamos alguna tecla en concreto, cambiemos la información mostrada en pantalla. Por otro lado, actualmente se muestra información genérica a excepción de las puertas y sensores de movimiento, que son los que hay en todo el sistema.

  Una idea que se ha obtenido es la de crear ya una función individual por estancia, es decir, que cada habitación tenga un conjunto de sensores que envien los datos a un _script_ propio de la habitación, lo que provocaría una simulación y experencia en la transferencia de datos más real, ya que es la idea que se tiene. Esto se haría de la siguiente manera:

  Crear un objeto _placa_ que contendrá todos los sensores de la habitación en cuestión, estos sensores, al esta bajo un mismo padre, enviarán la información a este. Cuando le llegue al padre, enviarlo a la placa general del sistema, o lo que es lo mismo, el padre de los padres, y que este procese la información para enviarla al servidor local (en este caso el localhost en unity) para que almacene los datos en la base de datos. Para la realización de esto, lo que puede hacer el padre de los sensores es obtener los datos que tiene los hijos (tengo que mirar la forma) y ya tratarlos.

  Enlaces interesantes para esto:

  - _https://docs.unity3d.com/ScriptReference/GameObject.SendMessage.html_
  - _https://docs.unity3d.com/ScriptReference/Events.UnityEvent.html_
  - _https://learn.unity.com/tutorial/eventos-w_

  Por otro lado, se ha observado que hay otra forma de crear la animación del movimientos de las puertas, esto es con la opción _animation_ de unity, lo que nos permite crear en base a cualquier objeto una animación que puede reutilizarse en todos los casos, por tanto se ha probado este método para realizar el movimiento de las puertas.

  Para la realización de esto, hay que crear 2 estados en la puerta, estado cerrada y abierta. Esto nos permitirá alternar el movimiento con la animación, después hay que crear un _clip_ del movimiento de la puerta y, finalmente, generar una variable que nos permita saber el estado de esta en todo momento.

  Cuando se realice todo esto, en nuestro objeto puerta lo que debemos de hacer es asociar la animación creada y crear un _script_ que cuando entremos en el _trigger_ de la puerta nos permita realizar la apertura, así como cambiar su estado de cara a la simulación.

  _https://www.youtube.com/watch?v=XIwW11OI_4E_

  ![Escena 1 puerta](/images/animationPuerta1.png "Puerta Cerrada")
  ![Escena 2 puerta](/images/AnimationPuerta2.png "Puerta Abierta")

  Como se puede apreciar, se ha recreado la animación de abrir y cerrar una puerta, esto nos servirá para utilizarlo en cada una de las puertas del domicilio que conectan estancias y, se ha creado una variable interna que nos permite saber el estado de la puerta, cosa que se leerá y mostrará en pantalla.

  Por otro lado, hay que crear la relación entre ambas escenas y combinando el estado de la variable del estado de la puerta.

  ![Esquema relación escenas](/images/estadoAnimacion.png "Estados animación")

  Para la ejecución de la animación es parecida a lo que se realizaba anteriormente, es decir, cuando el usuario con la etiqueta _player_ entre en el _trigger_ de la puerta, esta activará la animación cuando se de a la tecla de interacción deseada.

  Por otro lado, se ha averiguado que es posible recrear una máquina de estados que nos permita generar una especie de rutina para el usuario, es decir, una lógica que diga al usuario a donde ir en cada momento u hora del día con diferentes variables, lo que viene a ser crear una rutina del día a día con posibilidad de que ocurran diferentes situaciones mediante algún valor aleatorio, como puede ser una caída, inundación, incremento de temperatura, etc.

  _https://docs.unity3d.com/Manual/StateMachineBasics.html_

---

- **27/03/2024**

  Se ha implementado un primer prototípo del sensor de temperatura por estancia, esto se realiza leyendo la variable _tempActual_ que se encuentra en el script que controla el ciclo de día y noche, ya que calcula también la temperatura en base a la hora y la estación seleccionada, esto nos permite obtener los resultados en todo momento. Por tanto, con esta primera aproximación, se realizará un control de datos más exhaustivo que nos permitirá gestionar los datos a enviar.

  Por otro lado, se ha empezado a usar la jerarquia mencionada anteriormente, es decir, que por estancia haya un componente general que contendrá todos los sensores de esta misma. Estos sensores 'hijos' enviarán los datos a ese componente 'padre', de esta manera tendrá un uso más fiel a la realidad que queremos demostrar.

  También se ha incorporado todos los sensores de apertura de puertas, lo que nos permite saber el estado de cada una en todo momento.

  Finalmente, se ha incorporado una función que nos permite comparar temperaturas, ya que al tener una curva de valores en grados centigrados nos permitirá enviar los datos de una manera más sencilla, esto es debido a que queremos enviar datos única y exclusivamente cuando haya una variación interesante de valores (por eso se ha aproximado los valores a 2 decimales, aunque internamente el cálculo se realiza con unos 6 decimales) o cuando haya pasado un intervalo de tiempo especificado.

  A continuación se muestra la salida de los valores en la estancia del recibidor:

  ![Temperatura Recibida](/images/LecturaTemperaturaDiferencia.png "Temperatura Recibida")

  Como se puede observar, hay un cambio considerable en las horas en la que incrementa la temperatura, esto es debido a que guardamos la temperatura anterior para ir comparando con la actual (aproximación a 2 decimales), lo que nos permite ir comparando valores y ver si hay variación en un tiempo determinado.

---

- **30/03/2024**

  Nos hemos encontrado con un error que había previsto que podía pasar. Y es que a la hora de enviar los datos al recepetor, este espera únicamente un cliente desde unity, pero la cuestión es que como hay 2 valores enviándose en este momento el servidor no puede tratar estos datos a la par. Cosa que no es lo que queremos, por tanto, vamos a intentar crear la jerarquía de envío de datos para poder llegar a enviar todos los datos a la vez.

  La importancia de la jerarquía de objetos en Unity
  Al desarrollar proyectos en Unity, la jerarquía de objetos juega un papel vital en la organización y estructuración de su escena. Esta característica le permite administrar elementos de manera eficiente en un entorno de trabajo, lo que contribuye significativamente a la coherencia y el mantenimiento del código.

  - Organización y estructura: La jerarquía de objetos se basa en el concepto de objetos del juego, que son elementos básicos que actúan como contenedores de los distintos componentes y guiones que componen la escena. Estos objetos del juego se pueden organizar jerárquicamente para crear relaciones entre padres e hijos. Esta estructura de árbol facilita la agrupación de elementos relacionados en una organización lógicamente consistente de su escena.

  - Manipulación dirigida: El uso de padres e hijos en una jerarquía de objetos permite operaciones compartidas en elementos. Cuando cambias las propiedades de un GameObject principal, como su posición, rotación o escala, esos cambios se aplican por igual a todos sus GameObjects secundarios. Esta característica es especialmente útil cuando se trabaja con conjuntos complejos de objetos, como figuras con múltiples partes móviles o accesorios.
  - Comunicación entre componentes:Otra ventaja importante de la jerarquía de objetos es que facilita la comunicación entre diferentes componentes y guiones en una escena. Los objetos del juego pueden acceder a sus componentes padre e hijo, lo que permite un intercambio eficiente de datos y eventos. Esta función es fundamental para la implementación de la lógica del juego y la interacción de elementos.

  - Reciclaje y mantenimiento: Además de ser útiles durante el desarrollo, las jerarquías de objetos facilitan la reutilización y el mantenimiento del código. Los conjuntos de objetos del juego se pueden convertir en elementos prefabricados y preconfigurados que se pueden usar fácilmente en múltiples escenas y proyectos. Esto no sólo mejora la eficiencia del desarrollo, sino que también promueve la coherencia y consistencia en el diseño del juego.

  Por otro lado, se ha implementado una interfaz de padre que nos permite obtener los valores que envíen los hijos.
  El código implementado hace referencia a un componente llamado PadreReceiver que está diseñado para adjuntarse al objeto principal en Unity. Este componente tiene una función para recibir datos de sensores infantiles, como un sensor de temperatura o un controlador de puerta, y enviar estos datos a un servidor local mediante una conexión TCP. función principal:
  Recibir datos del sensor del niño:

  El componente PadreReceiver implementa la interfaz ISensorDataReciever, que define métodos para recibir datos de temperatura y estado de la puerta. Cuando el subsensor detecta un cambio en los datos de temperatura o el estado de la puerta, se llamará a los métodos ReceiverTempData y ReceiverDoorState, respectivamente. Estos métodos actualizan los últimos valores conocidos de temperatura y estado de la puerta y configuran los indicadores de cambio (Tempchanges y Doorchanges) para indicar que los datos han cambiado. Enviar datos al servidor:

  El elemento PadreReceiver comprueba periódicamente si han pasado cinco minutos desde la última actualización o si ha habido algún cambio en los datos del sensor.
  Si se cumplen estas condiciones, el mensaje se combina con los datos de estado y/o temperatura del puerto modificados. El mensaje se envía al servidor local a través de una conexión TCP en el puerto 8052. El formato del mensaje es SensorType:datos, donde SensorType puede ser "Temperatura" o "Puerta" y los datos son el valor actual correspondiente. Una vez enviados los datos, el indicador de cambio y la última hora de actualización se actualizan para una mayor comparación.

  El objetivo principal de este componente es proporcionar una interfaz centralizada para recibir y enviar datos del sensor en la aplicación del dispositivo. Al agrupar la lógica de control de datos en una instalación general, la gestión y el mantenimiento de los códigos se simplifican y la integración de nuevos sensores en el sistema. Además, el envío de datos al servidor permite su almacenamiento y posterior análisis, lo que puede resultar útil para monitorizar y controlar entornos virtuales o físicos.
- **06/04/2024**

  Se ha implementado una serie extra de funciones que deberá de declarar el padre para poder recibir datos. De esta manera se ha obtenido la función de recibir todos los datos de los sensores que contiene la placa de la habitación, por tanto, cada 'x' tiempo vamos recibiendo datos de todos los sensores. Una vez que nos llegan los datos, tenemos dos opciones para enviarlos al servidor, con una diferencia de tiempo de 5 minutos en la simulación (que depende de la velocidad a la que queremos que avance un día) o cuando haya algún tipo de cambio en alguno de los sensores, es decir, cada cierto tiempo o cada vez que haya alguna actualización de alguno de los sensores respecto a su valor anterior. Para esto, se obtiene el dato del sensor y se compara con el dato (del mismo sensor) que recibimos anteriormente. Esto nos permite enviar los datos al servidor en un formato determinado:

  -  Placa_Habitación(Hora_Interna);tipo_sensor_1:datos_1,tipo_sensor_2:datos_2,...

  ![Datos recibidos en el servidor](/images/DosPlacasYHoraNueva.png "Datos recibidos en el servidor")

  Esto nos permite enviar todos los datos de los sensores de golpe y, una vez recibidos en el servidor, tratarlos de la siguiente manera (aún no implementado):

  - Recibir datos.
  - Obtener los datos de la placa y sus datos mediante el separador ';'.
  - Separar los sensores entre ellos para diferenciarlos, mediante el separador ','.
  - Obtener el tipo de sensor y los datos de este, mediante el separado ':'.

  
  Con todo esto, podemos ir comparando datos en base a la hora del sistema y los datos obtenidos de cada sensor.

  Por otro lado, se ha añadido un nuevo diccionario en el servidor que recibirá los datos. Este contendrá el nombre de cada estancia como clave y otro diccionario que tendrá colas de datos para cada sensor, esto nos permitirá ir almacenando y tratando mediante un FCFS. Esto se ha decidido así para evitar saturar al servidor, ya que tendrá que recibir datos muy seguidamente y podría provocar la perdida de estos, por tanto, si los vamos almacenando en una cola para cada sensor y estancia, podemos tratarlos de diferentes maneras en diferentes momentos.

  Otra opción que se ha estando investigando, es el guardado de datos en ficheros de texto y después ir tratandolos mediante con algún otro proceso (aún hay que mirarlo bien).

  Otros cambios realizados han sido el de cambiar el formato de la hora, es decir, anteriormente calculabamos la hora y contabamos con esta mediante un nuevo decimal que iba del 0 al 100 para cambiar la hora, pero se ha usado la variable de tipo _TimeSpan_ que nos permite cambiar la hora al formato que deseamos. Este cambio de datos lo realiza el script que lleva el control del movimiento del sol y de la temperatura. En si, la lógica es la misma, calculamos la hora del sistema en formato decimal en función de la duración del día deseada y, después, cambia el formato en una nueva variable que se usará para enviar los datos al servidor y, de esta manera, como se puede apreciar en la imagen anterior, tener un formado de HH:MM:SS.

  También se ha optado por no mostrar mensajes de interacción entre el usuario real y el de la simulación, es decir, anteriormente cuando nos acercabamos a una puerta nos salía un mensaje indicando la tecla para abrir la puerta, pero esto se ha considerado innecesario ya que será una simulación y no un juego o algo del estilo. Aunque la función de la tecla quedará en el código para diferentes pruebas.

  Finalmente, se ha arreglado un problema al enviar la temperatura de la simulación cuando llegabamos a la máxima del día. Esto era porque, en la placa principal, comparabamos la temperaturas entre ellas y, si era diferente, pues entonces la enviabamos al servidor, cosa que es incorrecta y se ha solucionando comparando las horas donde la temperatura es máxima.

  Para la simulación, se ha pensado en que cuando el sujeto se acerque a una puerta, esta se abra automáticamente y, si sale del _trigger_ de la puerta, se cierre. Esto es para obtener datos de las puertas en movimiento también. Por otro lado, se quiere añadir luces en cada estancia y que, cuando el usuario entre en una estancia, estas se iluminen en caso de que el sensor de movimiento lo detecte. Para esto, se debe de tener en cuenta que el usuario puede quedarse en la habitación en cuestión en estático, cosa que debemos de considerar para no apagarlas antes de tiempo. Por otro lado, para el tema de la luminosidad, el sensor leera los valores de las _directional lights_ de cada estancia, ya que se incorporarán, y obtendrá un valor en base al movimiento y si se activan o no. También se ha pensado en que, depende de la hora y la posición del sol (que tenemos ambos valores) condicionar el encendido de la luz, es decir, obtener el valor de la luminosidad de las propias estancias para compararlo con un mínimo o máximo, lo que nos permitirá aumentar o no el valor de la propia luz.

  Se ha modificado el código del servidor para diferenciar los datos recibidos por tipo de habitación y sensores, de esta manera se podrá consultar y manejar mejor los datos a la hora de usarlos en la base de datos.

  ![Datos tratados](/images/DatosGuardados.png "Datos tratados")

  Como podemos observar, tenemos el nombre de la estancia como nombre del fichero, la hora al inicio de la tabla de los sensores y, dentro de esta tabla, el contenido que hemos recibido de cada sensor. Ahora queda comprobar las horas y los datos respecto a los anteriores datos obtenidos y así poder tener unos valores más reales.


**Avances futuros (+/- prioridad):**

- Crear las luces para cada estancia.

- Implementar sensor de luminosidad, presión, humedad, sonido y movimiento (ya implementado pero a falta de probar).

- Gestionar la recepción de los datos recibidos en el servidor, para guardarlo en ficheros o en colas.

- Implementar código de la API para la base de datos, tanto su creación como modificación.

- Crear circuito por donde se irá moviendo el usuario de manera automatizada (máquina de estados).

- Hacer una lógica de hábitos, donde dependiendo de la hora el usuario pueda moverse a una habitación en concreto o realizar diferentes tareas.

**Este proyecto se encuentra en desarrollo y se irán actualizando los avances en este archivo README.md.**
