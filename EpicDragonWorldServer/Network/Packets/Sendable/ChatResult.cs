﻿/**
 * Author: Pantelis Andrianakis
 * Date: November 7th 2018
 */
class ChatResult : SendablePacket
{
    public ChatResult(byte chatType, string sender, string message)
    {
        // Send the data.
        WriteShort(10); // Packet id.
        WriteByte(chatType); // 0 system, 1 normal chat, 2 personal message
        WriteString(sender);
        WriteString(message);
    }
}
