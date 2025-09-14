using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

//初始程序，创建文件夹，初始化两个重要列表
public class Initapp : MonoBehaviour {

    private string ex = Application.persistentDataPath + "/EX";                     //手机本地存放实验资源包文件夹
    private string exListPath = Utils.IP + "/dianzijimu/EX_List.txt";               //实验资源包列表下载路径





    //创建txt文本
    public void CreateFile(string spath, string name) {
        if (Directory.Exists(spath) && !File.Exists(spath + "/" + name)) {          //如果不存在该文件夹
            FileStream stream = File.Create(spath + "/" + name);                    //创建文件
            stream.Close();                                                         //如不及时关闭,短时间进行其他操作会有问题
        }
    }

	//删除txt文本
	public void RemoveFile(string spath, string name) {
		if (Directory.Exists(spath) && File.Exists(spath + "/" + name)) {          	//如果存在该文件
			File.Delete(spath + "/" + name);										//删除
		}
	}

    //读取txt中的信息
    public void loadText(string path, ArrayList al) {
        al.Clear();                                                                 //清空列表
		//初始化内嵌实验资源包 到本地实验包列表
        using (System.IO.StreamReader sr = new System.IO.StreamReader(
			path, Encoding.Default)) {
			string str;																//定义局部变量保存每行字符串
			while ((str = sr.ReadLine()) != null) {									//如果此行不为空
                string[] ss = str.Split(new char[] { ' ' });						//根据空格分隔字符串 
                al.Add(ss);                                                   		//读取文本中的内容到列表中
            }
        }
    }

	//读取txt中的信息
	public void localText(string path, ArrayList al) {
		al.Clear();                                                                 //清空列表
		//初始化内嵌实验资源包 到本地实验包列表
		al.Add(new string[] { "EX_0", "点亮世界之光" });
		al.Add(new string[] { "EX_1", "共享电流" });
		al.Add(new string[] { "EX_2", "绕行更省力" });
		al.Add(new string[] { "EX_3", "滑来滑去" });
		al.Add(new string[] { "EX_4", "此路不通" });
		al.Add(new string[] { "EX_5", "旋转的飞碟" });
		using (System.IO.StreamReader sr = new System.IO.StreamReader(
			path, Encoding.Default)) {
			string str;																//定义局部变量保存每行字符串
			while ((str = sr.ReadLine()) != null) {									//如果此行不为空
				string[] ss = str.Split(new char[] { ' ' });						//根据空格分隔字符串 
				al.Add(ss);                                                   		//读取文本中的内容到列表中
			}
		}
	}

    //从服务器下载文件并保存到本地
    IEnumerator downAsFile(string path, string filename, ArrayList al) {
        WWW www = new WWW(path);													//定义WWW类
        yield return www;															//返回www
        if (www.error == null) {													//如果解压完成
            byte[] stream = www.bytes;                                              //定义字节数组
            if (!File.Exists(filename)) {
                FileStream fs = new FileStream(filename, FileMode.CreateNew);       //定义FileStream
                BinaryWriter w = new BinaryWriter(fs);                              //定义BinaryWriter
                w.Write(stream);                                                    //写入字节数组
                fs.Close();                                                         //关闭FileStream
                w.Close();                                                          //关闭BinaryWriter										
            }
			loadText(filename, al);                                                 //通过服务器TXT文件初始化服务器实验包列表
        }
    }

}
