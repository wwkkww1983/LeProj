package com.apiof.picar;

import java.io.DataOutputStream;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;

import android.util.Log;

public class WebUtils {

	
	private static final int HTTP_200 = 200;
	private static final String TAG_GET = "Web GET";
	private static final String TAG_POST = "Web POST";


	// Get��ʽ����
	public static void requestByGet(String path) throws Exception {
		URL url = new URL(path);
		// ��һ��HttpURLConnection����
		HttpURLConnection urlConn = (HttpURLConnection) url.openConnection();
		// �������ӳ�ʱʱ��
		urlConn.setConnectTimeout(5 * 1000);
		// ��ʼ����
		urlConn.connect();
		// �ж������Ƿ�ɹ�
		if (urlConn.getResponseCode() == HTTP_200) {
			// ��ȡ���ص�����
			byte[] data = readStream(urlConn.getInputStream());
			Log.i(TAG_GET, "Get��ʽ����ɹ��������������£�");
			Log.i(TAG_GET, new String(data, "UTF-8"));
		} else {
			Log.i(TAG_GET, "Get��ʽ����ʧ��");
		}
		// �ر�����
		urlConn.disconnect();
	}
	
	
	// Post��ʽ����
	public static void requestByPost(String path) throws Throwable {
		// ����Ĳ���ת��Ϊbyte����
		String params = "id=" + URLEncoder.encode("t_stop", "UTF-8");
		byte[] postData = params.getBytes();
		// �½�һ��URL����
		URL url = new URL(path);
		// ��һ��HttpURLConnection����
		HttpURLConnection urlConn = (HttpURLConnection) url.openConnection();
		// �������ӳ�ʱʱ��
		urlConn.setConnectTimeout(5 * 10000);
		// Post������������������
		urlConn.setDoOutput(true);
		// Post������ʹ�û���
		urlConn.setUseCaches(false);
		// ����ΪPost����
		urlConn.setRequestMethod("POST");
		urlConn.setInstanceFollowRedirects(true);
		// ��������Content-Type
		urlConn.setRequestProperty("Content-Type",
				"application/x-www-form-urlencode");
		// ��ʼ����
		urlConn.connect();
		// �����������
		DataOutputStream dos = new DataOutputStream(urlConn.getOutputStream());
		dos.write(postData);
		dos.flush();
		dos.close();
		// �ж������Ƿ�ɹ�
		if (urlConn.getResponseCode() == HTTP_200) {
			// ��ȡ���ص�����
			byte[] data = readStream(urlConn.getInputStream());
			Log.i(TAG_POST, "Post����ʽ�ɹ��������������£�");
			Log.i(TAG_POST, new String(data, "UTF-8"));
		} else {
			Log.i(TAG_POST, "Post��ʽ����ʧ��");
		}
	}
	
	
	/**
	 * @���� ��ȡ��
	 * @param inStream
	 * @return �ֽ�����
	 * @throws Exception
	 */
	public static byte[] readStream(InputStream inStream) throws Exception {
		int count = 0;
		while (count == 0) {
			count = inStream.available();
		}
		byte[] b = new byte[count];
		inStream.read(b);
		return b;
	}
}
