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

  - **Precio componentes habitación:**

  - **Precio componentes cocina:**

  - **Precio componentes baño:**

**Avances futuros:**

- Implementar código de la API para la base de datos, tanto su creación como modificación.
- Creación de un esquema de los componentes electrónicos en base al plano de la vivienda para ver como situar cada uno de ellos.
- También se quiere estudiar la forma de implementar un ciclo de día y noche, para simular la temperatura, humedad y presión atmosférica.
- Desarrollo del proyecto general para la incorporación completa de puertas en todas las estancias.
- Mejorar gráficamente el escenario.

**Este proyecto se encuentra en desarrollo y se irán actualizando los avances en este archivo README.md.**
