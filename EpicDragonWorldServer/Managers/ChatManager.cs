﻿/**
 * Author: Pantelis Andrianakis
 * Date: November 7th 2018
 */
class ChatManager
{
    static readonly byte CHAT_TYPE_SYSTEM = 0;
    static readonly byte CHAT_TYPE_NORMAL = 1;
    static readonly byte CHAT_TYPE_MESSAGE = 2;
    static readonly string COMMAND_PERSONAL_MESSAGE = "/tell ";
    static readonly string COMMAND_LOCATION = "/loc";
    static readonly string SYS_NAME = "System";
    static readonly string MSG_TO = "To ";

    public static void HandleChat(Player sender, string message)
    {
        // Check if message is empty.
        message = message.Trim();
        if (message.Length == 0)
        {
            return;
        }

        string lowercaseMessage = message.ToLowerInvariant().Replace("\\s{2,}", " "); // Also remove all double spaces.
        if (lowercaseMessage.Equals(COMMAND_LOCATION))
        {
            LocationHolder location = sender.GetLocation();
            sender.ChannelSend(new ChatResult(CHAT_TYPE_SYSTEM, SYS_NAME, "Your location is " + location.GetX() + " " + location.GetY() + " " + location.GetZ()));
        }
        else if (lowercaseMessage.StartsWith(COMMAND_PERSONAL_MESSAGE))
        {
            string[] lowercaseMessageSplit = lowercaseMessage.Split(" ");
            if (lowercaseMessageSplit.Length < 3) // Check for parameters.
            {
                sender.ChannelSend(new ChatResult(CHAT_TYPE_SYSTEM, SYS_NAME, "Incorrect syntax. Use /tell [name] [message]."));
                return;
            }

            Player receiver = WorldManager.GetPlayerByName(lowercaseMessageSplit[1]);
            if (receiver == null)
            {
                sender.ChannelSend(new ChatResult(CHAT_TYPE_SYSTEM, SYS_NAME, "Player was not found."));
            }
            else
            {
                // Step by step cleanup, to avoid problems with extra/double spaces on original message.
                message = message.Substring(lowercaseMessageSplit[0].Length, message.Length).Trim(); // Remove command.
                message = message.Substring(lowercaseMessageSplit[1].Length, message.Length).Trim(); // Remove receiver name.
                sender.ChannelSend(new ChatResult(CHAT_TYPE_MESSAGE, MSG_TO + receiver.GetName(), message));
                receiver.ChannelSend(new ChatResult(CHAT_TYPE_MESSAGE, sender.GetName(), message));
                // Log chat.
                if (Config.LOG_CHAT)
                {
                    LogManager.LogChat("[" + sender.GetName() + "] to [" + receiver.GetName() + "] " + message);
                }
            }
        }
        else // Normal message.
        {
            sender.ChannelSend(new ChatResult(CHAT_TYPE_NORMAL, sender.GetName(), message));
            foreach (Player player in WorldManager.GetVisiblePlayers(sender))
            {
                player.ChannelSend(new ChatResult(CHAT_TYPE_NORMAL, sender.GetName(), message));
            }
            // Log chat.
            if (Config.LOG_CHAT)
            {
                LogManager.LogChat("[" + sender.GetName() + "] " + message);
            }
        }
    }
}
