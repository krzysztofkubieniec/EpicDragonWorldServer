/*
 * This file is part of the Epic Dragon World project.
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 */
package com.epicdragonworld.gameserver.network;

import java.util.concurrent.RejectedExecutionException;
import java.util.logging.Logger;

import io.netty.buffer.Unpooled;
import io.netty.channel.Channel;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.SimpleChannelInboundHandler;
import io.netty.util.CharsetUtil;

/**
 * @author Pantelis Andrianakis
 */
public class ClientHandler extends SimpleChannelInboundHandler<String>
{
	private static final Logger LOGGER = Logger.getLogger(ClientHandler.class.getName());
	
	private Channel _channel;
	private String _ip;
	private String _accountName;
	
	@Override
	public void handlerAdded(ChannelHandlerContext ctx)
	{
		final Channel incoming = ctx.channel();
		_channel = incoming;
		_ip = incoming.remoteAddress().toString();
		LOGGER.info(getClass().getSimpleName() + ": New connection[" + _ip + "]");
	}
	
	@Override
	public void handlerRemoved(ChannelHandlerContext ctx)
	{
		LOGGER.info(getClass().getSimpleName() + ": Connection closed! [" + ctx.channel().remoteAddress() + "]");
		// TODO: ThreadPoolManager.execute(new DisconnectTask());
	}
	
	public void send(String message)
	{
		_channel.writeAndFlush(Unpooled.copiedBuffer(message + "\r\n", CharsetUtil.UTF_8));
	}
	
	@Override
	protected void channelRead0(ChannelHandlerContext ctx, String data)
	{
		LOGGER.info("Recieved " + data);
	}
	
	@Override
	public void channelInactive(ChannelHandlerContext ctx)
	{
		LOGGER.finer("Client Disconnected: " + ctx.channel());
		
		// no long running tasks here, do it async
		try
		{
			// TODO: ThreadPoolManager.execute(new DisconnectTask());
		}
		catch (RejectedExecutionException e)
		{
			// server is closing
		}
	}
	
	@Override
	public void exceptionCaught(ChannelHandlerContext ctx, Throwable cause)
	{
	}
	
	public Channel getChannel()
	{
		return _channel;
	}
	
	public String getIp()
	{
		return _ip;
	}
	
	public String getAccountName()
	{
		return _accountName;
	}
}