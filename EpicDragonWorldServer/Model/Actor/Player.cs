﻿using MySql.Data.MySqlClient;
using System;

/**
 * Author: Pantelis Andrianakis
 * Date: November 7th 2018
 */
class Player : Creature
{
    static readonly string RESTORE_CHARACTER = "SELECT * FROM characters WHERE name=@name";
    static readonly string STORE_CHARACTER = "UPDATE characters SET name=@name, class_id=@class_id WHERE account=@account AND name=@name";

    readonly GameClient client;
    readonly string name;
    readonly int classId;

    public Player(GameClient client, string name)
    {
        this.client = client;
        this.name = name;

        // Load information from database.
        try
        {
            MySqlConnection con = DatabaseManager.GetConnection();
            MySqlCommand cmd = new MySqlCommand(RESTORE_CHARACTER, con);
            cmd.Parameters.AddWithValue("name", name);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                classId = reader.GetInt32("class_id");
                GetLocation().SetX(reader.GetFloat("x"));
                GetLocation().SetY(reader.GetFloat("y"));
                GetLocation().SetZ(reader.GetFloat("z"));
                // TODO: Restore player stats (STA/STR/DEX/INT).
                // TODO: Restore player level.
                // TODO: Restore player Current HP.
                // TODO: Restore player Current MP.
            }
            con.Close();
        }
        catch (Exception e)
        {
            LogManager.Log(e.ToString());
        }
    }

    public void StoreMe()
    {
        try
        {
            MySqlConnection con = DatabaseManager.GetConnection();
            MySqlCommand cmd = new MySqlCommand(STORE_CHARACTER, con);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("class_id", classId);
            // TODO: Save location.
            // TODO: Save player stats (STA/STR/DEX/INT).
            // TODO: Save player level.
            // TODO: Save player Current HP.
            // TODO: Save player Current MP.
            cmd.Parameters.AddWithValue("account", client.GetAccountName());
            // Parameter already added above?
            // cmd.Parameters.AddWithValue("name", _name);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception e)
        {
            LogManager.Log(e.ToString());
        }
    }

    public GameClient GetClient()
    {
        return client;
    }

    public string GetName()
    {
        return name;
    }

    public int GetClassId()
    {
        return classId;
    }

    public void ChannelSend(SendablePacket packet)
    {
        client.ChannelSend(packet);
    }

    public override bool IsPlayer()
    {
        return true;
    }

    public override Player AsPlayer()
    {
        return this;
    }
}
