using System;
using UnityEngine;
using System.Collections;

public class PlayerMovementRoutine : MonoBehaviour
{
    public Transform[] waypoints;
    private float speed;

    private int currentWaypointIndex = 8; // Inicio en Habitación 2.1
    private Coroutine routineCoroutine;
    private System.Random random = new System.Random();
    private int desayunarEn = -1; // Declarar desayunarEn fuera de Update y inicializar a -1
    void Start()
    {
        transform.position = waypoints[8].position;
        speed = 10000000;
    }
    void Update()
    {
        // Obtener la hora actual en formato TimeSpan
        TimeSpan horaActual = CicloDN.horaFormateada;
        
    }

    IEnumerator SeguirRutina()
    {
        while (true) // Bucle infinito para repetir la rutina cada día
        {
            // Despertarse e ir al baño (6:00 - 8:00)
            yield return MoverAPunto(11, 6f + random.Next(120) / 60f, new int[] { 6, 5, 11 });
            yield return EsperarMinutos(5 + random.Next(10)); // 5-15 minutos en el baño

            // Recoger la habitación (10-20 minutos)
            yield return MoverAPunto(8, 0f, new int[] { 5, 6, 8 }); 
            yield return EsperarMinutos(10 + random.Next(10));

            // Preparar el desayuno (10-15 minutos)
            yield return MoverAPunto(2, 0f, new int[] { 6, 5, 4, 3, 1, 2 });
            yield return EsperarMinutos(10 + random.Next(5));

            // Desayunar (20-30 minutos)
            int desayunarEn = random.Next(2); // 0: Cocina, 1: Salón
            if (desayunarEn == 1)
            {
                yield return MoverAPunto(3, 0f, new int[] { 1, 3 });
            }
            yield return EsperarMinutos(20 + random.Next(10));

            // Limpiar los platos (5-10 minutos)
            if (desayunarEn == 1)
            {
                yield return MoverAPunto(2, 0f, new int[] { 1, 2 });
                yield return EsperarMinutos(5 + random.Next(5));
            }

            // Descansar y ver la tele en el salón hasta la hora de comer (13:00 - 14:00)
            yield return MoverAPunto(3, 0f, new int[] { 1, 3 });
            yield return EsperarHasta(13f + random.Next(60) / 60f);

            // Ir al baño (opcional)
            if (random.Next(2) == 0) // 50% de probabilidad de ir al baño
            {
                yield return MoverAPunto(11, 0f, new int[] { 4, 5, 11 });
                yield return EsperarMinutos(5 + random.Next(10));
                yield return MoverAPunto(3, 0f, new int[] { 5, 4, 3 });
            }

            // Preparar la comida (30-60 minutos)
            yield return MoverAPunto(2, 0f, new int[] { 1, 2 });
            yield return EsperarMinutos(30 + random.Next(30));

            // Comer en el salón (30-60 minutos)
            yield return MoverAPunto(3, 0f, new int[] { 1, 3 });
            yield return EsperarMinutos(30 + random.Next(30));

            // Trabajar o estudiar en el salón (4 horas)
            yield return MoverAPunto(3, 0f, new int[] { 1, 3 });
            yield return EsperarHasta(17f); 

            // Ir al baño (opcional)
            if (random.Next(2) == 0) // 50% de probabilidad de ir al baño
            {
                yield return MoverAPunto(11, 0f, new int[] { 4, 5, 11 });
                yield return EsperarMinutos(5 + random.Next(10));
                yield return MoverAPunto(3, 0f, new int[] { 5, 4, 3 });
            }

            // Tiempo libre en el salón (1 hora)
            yield return EsperarHasta(18f);

            // Preparar la cena (30-60 minutos)
            yield return MoverAPunto(2, 0f, new int[] { 1, 2 });
            yield return EsperarMinutos(30 + random.Next(30));

            // Cenar en el salón (30-60 minutos)
            yield return MoverAPunto(3, 0f, new int[] { 1, 3 });
            yield return EsperarMinutos(30 + random.Next(30));

            // Tiempo libre en el salón (2 horas)
            yield return EsperarHasta(22f);

            // Ir a dormir (en la habitación 2.1)
            yield return MoverAPunto(8, 0f, new int[] { 4, 6, 8 });

            // Esperar hasta las 00:00 del día siguiente
            while (CicloDN.Hora < 24f)
            {
                yield return null;
            }
        }
    }

    IEnumerator MoverAPunto(int waypointIndex, float horaDestino ,int[] ruta = null)
    {
        if (horaDestino > 0f)
        {
            yield return EsperarHasta(horaDestino);
        }
        
        // Si no se proporciona una ruta, generar una ruta por defecto al destino
        if (ruta == null)
        {
            ruta = new int[] { waypointIndex }; 
        }

        // Moverse a lo largo de la ruta en un solo frame
        for (int i = 0; i < ruta.Length; i++)
        {
            int siguienteWaypoint = ruta[i];
            while (transform.position != waypoints[siguienteWaypoint].position)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    waypoints[siguienteWaypoint].position,
                    speed * Time.deltaTime
                );
                yield return null; // Esperar al siguiente frame
            }
            Debug.Log($"CurrentPosition: {waypoints[currentWaypointIndex].name} ---> NextWaypoint: {waypoints[siguienteWaypoint].name}");
            currentWaypointIndex = siguienteWaypoint; // Actualizar el índice actual
        }
    }
    IEnumerator EsperarHasta(float horaObjetivo)
    {
        TimeSpan horaObjetivoTS = TimeSpan.FromHours(horaObjetivo); // Convertir horaObjetivo a TimeSpan
        while (CicloDN.horaFormateada < horaObjetivoTS)
        {
            yield return null;
        }
    }
    IEnumerator EsperarMinutos(float minutos)
    {
        TimeSpan tiempoInicial = CicloDN.horaFormateada; // Obtener la hora actual al inicio de la espera
        TimeSpan tiempoObjetivo = tiempoInicial.Add(TimeSpan.FromMinutes(minutos)); // Calcular la hora objetivo

        while (CicloDN.horaFormateada < tiempoObjetivo) 
        {
            yield return null;
        }
    }
    int[] ObtenerRutaPredefinida(int inicio, int destino)
    {
        // Definición manual de rutas según el nuevo esquema
        switch (inicio)
        {
            case 0: // Recibidor1
                switch (destino)
                {
                    case 1: return new int[] { 1 };      // Recibidor2
                    case 10: return new int[] { 10 };    // Habitacion4.1
                }
                break;
            case 1: // Recibidor2
                switch (destino)
                {
                    case 0: return new int[] { 0 };      // Recibidor1
                    case 2: return new int[] { 2 };      // Cocina1
                    case 3: return new int[] { 3 };      // Salon
                }
                break;
            case 2: // Cocina1
                return new int[] { 1 };                  // Recibidor2
            case 3: // Salon
                switch (destino)
                {
                    case 1: return new int[] { 1 };      // Recibidor2
                    case 4: return new int[] { 4 };      // Pasillo1
                }
                break;
            case 4: // Pasillo1
                switch (destino)
                {
                    case 3: return new int[] { 3 };      // Salon
                    case 5: return new int[] { 5 };      // Pasillo2
                    case 7: return new int[] { 7 };      // Habitacion1.1
                }
                break;
            case 5: // Pasillo2
                switch (destino)
                {
                    case 4: return new int[] { 4 };      // Pasillo1
                    case 6: return new int[] { 6 };      // Pasillo3
                    case 11: return new int[] { 11 };    // Aseo
                }
                break;
            case 6: // Pasillo3
                switch (destino)
                {
                    case 5: return new int[] { 5 };      // Pasillo2
                    case 8: return new int[] { 8 };      // Habitacion2.1
                    case 9: return new int[] { 9 };      // Habitacion3.1
                }
                break;
            case 7: // Habitacion1.1
                return new int[] { 4 };                  // Pasillo1
            case 8: // Habitacion2.1
                return new int[] { 6 };                  // Pasillo3
            case 9: // Habitacion3.1
                return new int[] { 6 };                  // Pasillo3
            case 10: // Habitacion4.1
                return new int[] { 0 };                  // Recibidor1
            case 11: // Aseo
                return new int[] { 5 };                  // Pasillo2
        }
        return new int[] { destino }; // No debería ocurrir (mover directamente)
    }
}
