package com.weilaifeng.ipower.gate.cache.command;

import java.net.Inet4Address;
import java.util.List;
import java.util.concurrent.BlockingQueue;

import com.weilaifeng.ipower.jna.JNA;
import com.weilaifeng.ipower.master.constant.ConstantValue;
import com.weilaifeng.ipower.master.domain.ChannelData;
import com.weilaifeng.ipower.master.domain.GateHeader;
import com.weilaifeng.ipower.master.domain.SocketData;
import com.weilaifeng.ipower.master.utils.CommonUtil;

import io.netty.buffer.ByteBuf;

public class SetChannelCommaThread implements Runnable {

	private static final int LENGTH = 29;

	private BlockingQueue<ChannelData> down2TmnlQueue;

	private List<String> commAddrs;

	private String mainIp;

	private int mainPort;

	private String backupIp;

	private int backupPort;

	public SetChannelCommaThread(BlockingQueue<ChannelData> down2TmnlQueue, List<String> commAddrs, String mainIp,
			int mainPort, String backupIp, int backupPort) {
		this.down2TmnlQueue = down2TmnlQueue;
		this.commAddrs = commAddrs;
		this.mainIp = mainIp;
		this.mainPort = mainPort;
		this.backupIp = backupIp;
		this.backupPort = backupPort;
	}

	@Override
	public void run() {
		for (String commAddr : commAddrs) {
			try {
				ByteBuf buf = createGateHead(commAddr);
				ChannelData channelData = new ChannelData(new SocketData(buf));
				down2TmnlQueue.put(channelData);
			} catch (Exception e) {
				e.printStackTrace();
			}

		}
	}

	private ByteBuf createGateHead(String commAddr) throws Exception {
		/**
		 * 创建直接内存形式的ByteBuf，不能使用array()方法，但效率高
		 */
		ByteBuf out = CommonUtil.getByteBuf();
		GateHeader headBuf = new GateHeader(25);
		// 帧头
		headBuf.writeInt8(Integer.valueOf(ConstantValue.GATE_HEAD_DATA).byteValue());
		headBuf.writeInt8(Integer.valueOf(ConstantValue.GATE_HEAD_DATA).byteValue());
		headBuf.writeInt8(Integer.valueOf(ConstantValue.GATE_HEAD_DATA).byteValue());
		headBuf.writeInt8(Integer.valueOf(ConstantValue.DOWN_HEAD_DATA).byteValue());
		headBuf.writeInt8((byte) (LENGTH & 0xFF));// 数据包长度
		headBuf.writeInt8((byte) (0 & 0xFF));// 保留位置
		headBuf.writeInt8((byte) (4 & 0xFF));// 报文类型
		headBuf.writeInt8((byte) (0 & 0xFF));// 报文格式版本
		headBuf.writeInt32(Integer.parseInt(commAddr));// 终端通信地址
		// 状态查询报文开始
		byte[] m = Inet4Address.getByName(mainIp).getAddress();// 报文
		headBuf.writeInt8(m[0]);
		headBuf.writeInt8(m[1]);
		headBuf.writeInt8(m[2]);
		headBuf.writeInt8(m[3]);
		headBuf.writeInt16(mainPort);// 报文
		byte[] bs = Inet4Address.getByName(backupIp).getAddress();// 报文
		headBuf.writeInt8(bs[0]);
		headBuf.writeInt8(bs[1]);
		headBuf.writeInt8(bs[2]);
		headBuf.writeInt8(bs[3]);
		headBuf.writeInt16(backupPort);// 报文
		// 状态查询报文结束
		headBuf.writeInt8((byte) (JNA.CRC8Cal(headBuf.getDataBuffer(), 24) & 0xFF));// CRC8 校验码
		headBuf.writeInt8(Integer.valueOf(ConstantValue.GATE_HEAD_DATA).byteValue());
		headBuf.writeInt8(Integer.valueOf(ConstantValue.GATE_HEAD_DATA).byteValue());
		headBuf.writeInt8(Integer.valueOf(ConstantValue.GATE_HEAD_DATA).byteValue());
		headBuf.writeInt8(Integer.valueOf(ConstantValue.END_DATA).byteValue());
		out.writeBytes(headBuf.getDataBuffer());
		return out;
	}

}
