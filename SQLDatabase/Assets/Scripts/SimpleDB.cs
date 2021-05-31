using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;

public class SimpleDB : MonoBehaviour
{

    public Text texto;
    private string dbName = "URI=file:INVENTORYDB.db";

    // Start is called before the first frame update
    void Start()
    {
        CreateDB();
        

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.S))
        CreateDB();

        if(Input.GetKey(KeyCode.L))
        ShowItems();  

        if(Input.GetKey(KeyCode.D))
         DeleteItems();

        if(Input.GetKey(KeyCode.N))
        ponerarmas();

        if(Input.GetKey(KeyCode.M))
        ponerarmaduras();
    }

    public void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))  // Se utiliza una variable para crear una conexión con la base de datos a través de la .dll de Mono.Data.Sqlite
        {
            connection.Open();  // Abrimos la conexión

            using (var command = connection.CreateCommand())  // Insertar comando.
            {
                Debug.Log("Saving in to: " + dbName);
                command.CommandText = "CREATE TABLE IF NOT EXISTS inventory (ItemName VARCHAR(20), STA int, STR int);";    // Creacción de una nueva tabla en caso de que no haya ninguna, en este caso el inventario.
                command.ExecuteNonQuery();  // Realiza el comando en la Database.
            }
            connection.Close(); // Cierra la conexión
        }
    }

     public void AddItem(string ItemName, int STA, int STR)
    {
        using (var connection = new SqliteConnection(dbName))  // Se utiliza una variable para crear una conexión con la base de datos a través de la .dll de Mono.Data.Sqlite
        {
            connection.Open();  // Abrimos la conexión

            using (var command = connection.CreateCommand())  // Se utiliza una variable para insertar un comando en SQLite
            {
                command.CommandText = " INSERT INTO inventory (ItemName, STA, STR) VALUES ('" + ItemName + "', '" + STA + "' , '" + STR + "');";    //Crea una tabla si no existe una previa, llamada productsTable que contiene en su interior un VARCHAR de 20 caracteres ASCII y un float para determinar el precio.
                command.ExecuteNonQuery();  // Ejecuta el comando en SQL en la query de la DB. 
            }
            connection.Close(); // Cierra la conexión
        }
    }

    public void ShowItems()
    {
        
         texto.text = "";

        using (var connection = new SqliteConnection(dbName))  // Se utiliza una variable para crear una conexión con la base de datos a través de la .dll de Mono.Data.Sqlite
        {
            connection.Open();  // Abre la conexión con SQL

            using (var command = connection.CreateCommand())  // Se utiliza una variable para insertar un comando en SQLite
            {
                command.CommandText = "SELECT * FROM inventory;";   // Lee todo de la tabla productsTable

                using (IDataReader reader = command.ExecuteReader())    // Utilizamos el lector de datos de System.Data para ejecutar un comando de lectura
                {
                    while (reader.Read())   // Mientras existan datos para leer, ejecutará la siguiente linea
                        texto.text += reader["ItemName"] + "\t\t" +  reader["STA"] + "\t\t" + reader["STR"] +"\n"; // Introduce los valores del lector en el texto a mostrar por pantalla en Unity
                    reader.Close(); //Cierra el lector de SQL
                }
            }
            connection.Close(); // Cierra la conexión
        }
    }
public void DeleteItems()
    {
        
         texto.text = "";

        using (var connection = new SqliteConnection(dbName))  // Se utiliza una variable para crear una conexión con la base de datos a través de la .dll de Mono.Data.Sqlite
        {
            connection.Open();  // Abre la conexión con SQL

            using (var command = connection.CreateCommand())  // Se utiliza una variable para insertar un comando en SQLite
            {
                command.CommandText = "DROP TABLE inventory;";
                command.ExecuteNonQuery();
                texto.text = "VACIO";
                CreateDB();
            }
            connection.Close(); // Cierra la conexión
        }
    }

    public void ponerarmas()
    {
        CreateDB();
        AddItem("Iron Sword", 10, 15); 
        AddItem("Golden Sword",20, 30);
        AddItem("Platinum Axe", 30, 40); 
        ShowItems();
    }

        public void ponerarmaduras()
    {
        AddItem("Iron Helmet", 13, 26); 
        AddItem("Golden Chest",27, 42);
        AddItem("Platinum Guardbrace", 50, 74); 
        CreateDB();
        ShowItems();
        
    }
    
}
