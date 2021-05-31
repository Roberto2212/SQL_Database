using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;

/// <summary>
/// Este Script contiene las acciones de mostrado, guardado, borrado y cargado, enfocado a un inventario.
/// </summary>

public class SimpleDB : MonoBehaviour
{

    public Text texto;
    private string dbName = "URI=file:INVENTORYDB.db";


    void Start()
    {
        CreateDB();
        

    }

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
        using (var connection = new SqliteConnection(dbName))  // Se utiliza tiliza una variable para conectar con la base de datos.
        {
            connection.Open();  // Se abre la conexión

            using (var command = connection.CreateCommand())  // Se inserta el comando a través de una variable.
            {
                Debug.Log("Saving in to: " + dbName);
                command.CommandText = "CREATE TABLE IF NOT EXISTS inventory (ItemName VARCHAR(20), STA int, STR int);";    // Se crea una nueva tabla en caso de que no haya ninguna, en este caso el inventario.
                command.ExecuteNonQuery();  // Se realiza el comando en la Database.
            }
            connection.Close(); // Se cierra la conexión.
        }
    }

     public void AddItem(string ItemName, int STA, int STR)
    {
        using (var connection = new SqliteConnection(dbName))  // Se utiliza una variable para conectar con la base de datos.
        {
            connection.Open();  // Se abre la conexión.

            using (var command = connection.CreateCommand())  // Se inserta el comando a través de una variable.
            {
                command.CommandText = " INSERT INTO inventory (ItemName, STA, STR) VALUES ('" + ItemName + "', '" + STA + "' , '" + STR + "');";    // Se crea una nueva tabla en caso de que no haya ninguna, en este caso el inventario, que contiene los objetos y sus estadisticas.
                command.ExecuteNonQuery();  // Se realiza el comando en la Database.
            }
            connection.Close(); // Se cierra la conexión.
        }
    }

    public void ShowItems()
    {
        
         texto.text = "";

        using (var connection = new SqliteConnection(dbName))  // Se utliza una variable para conectar con la base de datos.
        {
            connection.Open();  // Se abre la conexión.

            using (var command = connection.CreateCommand())  // Se inserta el comando a través de una variable.
            {
                command.CommandText = "SELECT * FROM inventory;";   // See lee todo el inventario.

                using (IDataReader reader = command.ExecuteReader())    // Se utiliza IDataReader para un comando de lectura.
                {
                    while (reader.Read())   // Se ejecuta lo siguiente:
                        texto.text += reader["ItemName"] + "\t\t" +  reader["STA"] + "\t\t" + reader["STR"] +"\n"; // Se leen los valores de los objetos.
                    reader.Close(); // Se cierra el reader.
                }
            }
            connection.Close(); // Se cierra la conexión.
        }
    }
public void DeleteItems()
    {
        
         texto.text = "";

        using (var connection = new SqliteConnection(dbName))  // Se utiliza una variable para conectar con la base de datos.
        {
            connection.Open();  // Se abre la conexión.

            using (var command = connection.CreateCommand())  // Se inserta el comando a través de una variable.
            {
                command.CommandText = "DROP TABLE inventory;";
                command.ExecuteNonQuery();
                texto.text = "VACIO";
                CreateDB();
            }
            connection.Close(); // Se cierra la conexión.
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
