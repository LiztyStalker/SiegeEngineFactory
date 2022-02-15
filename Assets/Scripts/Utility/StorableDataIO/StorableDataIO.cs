namespace Utility.IO
{

    using UnityEngine;
    using System;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Runtime.Serialization;
    using System.IO;
    using System.Security.Cryptography;
    using System.Xml.Serialization;

    public enum TYPE_IO_RESULT
    {
        InProgress = 0,
        Success = 1,
        ConnectionError = 2,
        ProtocolError = 3,
        DataProcessingError = 4
    };



    public class StorableDataIO
    {

        private static StorableDataIO _current;

        public static StorableDataIO Current
        {
            get
            {
                if (_current == null)
                    _current = new StorableDataIO();
                return _current;
            }
        }



        byte[] key = new byte[24];// = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4};// new byte[8];// {1, 1, 1, 1, 1, 1, 1, 1};

        const int myIterations = 1000;

        //암호화 알고리즘 랜덤 난수
        RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        //암호 데이터
        byte[] salt = new byte[96];//{8, 7, 6, 5, 4, 3, 2, 1};//System.Text.Encoding.ASCII.GetBytes ("saltTestTextData");


        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider(); // 키24개 필요
                                                                                    //	DESCryptoServiceProvider des = new DESCryptoServiceProvider(); // 키 8개 필요


        string filePath { get { return Application.persistentDataPath; } }


        //	FileStream file;
        //	CryptoStream cryptoStream;

        public StorableDataIO()
        {
            //저장 및 불러오기
            //파일 저장 위치
            //m_filePath = Application.persistentDataPath;

            //            Debug.Log("FilePath : " + m_filePath);

            //키 가져오기 - 시스템 디바이스의 고유번호는 현재 고유번호의 인덱스값
            key = System.Text.Encoding.ASCII.GetBytes(SystemInfo.deviceUniqueIdentifier.Substring(0, 24));
            //123456789
            //

            //키의 길이만큼 반복
            for (int i = 0; i < key.Length; i++)
            {
                //삽입 - 고유번호가 인덱스가 되어 현재 고유번호의 인덱스 값을 가져옴.
                key[i] = (byte)(Convert.ToByte(SystemInfo.deviceUniqueIdentifier[(int)key[i] % SystemInfo.deviceUniqueIdentifier.Length]) - 32);
            }

            //솔트값 삽입
            rngCsp.GetBytes(salt);
            //Debug.Log("rngCsp : " + Convert.ToBase64String(salt));
        }


        //	private byte[] getKey(){
        ////		SystemInfo.deviceUniqueIdentifier;
        ////		key[] = 
        //
        //	}

        /// <summary>
        /// 계정 해쉬 데이터 가져오기
        /// 없을 경우 딱 한번만 실행
        /// </summary>
        /// <returns>The account hash.</returns>
        //	public string getAccountHash(string hash){
        //
        //		//초반 생성일 경우
        //		if (hash == null) {
        //
        //		} 
        //
        //		return hash;
        //	}

        /// <summary>
        /// 파일의 유무 판별
        /// </summary>
        /// <returns><c>true</c>, if file was ised, <c>false</c> otherwise.</returns>
        /// <param name="fileName">File name.</param>
        public bool isFile(string fileName)
        {
            return File.Exists(string.Format("{0}/{1}.dat", filePath, fileName));
        }


        /// <summary>
        /// 암호화 하기 .dat
        /// </summary>
        /// <param name="filePath">File path.</param>
        /// <param name="fileName">File name.</param>
        //	public CryptoStream encrypting(string filePath, string fileName){
        //		return encrypting(filePath, fileName, "dat");
        //	}
        //
        //	public CryptoStream encrypting(string filePath, string fileName, string fileExt){
        //		using(file = new FileStream(string.Format("{0}/{1}.{2}", m_filePath, fileName, fileExt), FileMode.Create, FileAccess.Write)){
        //			using(CryptoStream cryptoStream = new CryptoStream(file, tdes.CreateEncryptor(key, salt), CryptoStreamMode.Write)){
        //				//					using(CryptoStream cryptoStream = new CryptoStream(file, des.CreateEncryptor(key, salt), CryptoStreamMode.Write))
        //				return cryptoStream;
        //			}
        //		}
        //		return null;
        //	}


        /// <summary>
        /// 저장 데이터 변환하기 
        /// Serial -> Byte
        /// </summary>
        /// <param name="storableData"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] DataConvertSerialToByte(StorableData storableData)
        {
            byte[] data = new byte[0];

            try
            {

                using (MemoryStream memory = new MemoryStream())
                {
                    IFormatter bf = new BinaryFormatter();
                    bf.Serialize(memory, storableData);
                    data = memory.ToArray();
                    memory.Close();
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Serial -> Byte 변환 오류 : " + e.Message);
            }

            return data;
        }

        /// <summary>
        /// 저장 데이터 변환하기 
        /// Serial -> string
        /// </summary>
        /// <param name="storableData"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string DataConvertSerialToString(StorableData storableData)
        {
            string data = "";

            try
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    IFormatter bf = new BinaryFormatter();
                    bf.Serialize(memory, storableData);
                    data = Convert.ToBase64String(memory.ToArray());
                    memory.Close();
                }
            }
            catch (Exception e)
            {
                //Prep.LogError("Serial -> string 변환 오류 : ", e.Message, GetType());
            }

            return data;
        }

        /// <summary>
        /// 저장된 데이터 변환하기
        /// string -> Serial 변환
        /// </summary>
        /// <param name="dataString"></param>
        /// <returns></returns>
        public StorableData DataConvertStringToSerial(string dataString)
        {
            try
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    byte[] data = Convert.FromBase64String(dataString);
                    Debug.Log("length : " + BitConverter.ToString(data));
                    memory.Write(data, 0, data.Length);
                    memory.Position = 0;


                    IFormatter bf = new BinaryFormatter();

                    string str = Convert.ToBase64String(memory.ToArray());
                    Debug.Log("convert : " + str);

                    StorableData accountData = (StorableData)bf.Deserialize(memory);

                    memory.Close();
                    return accountData;


                }

            }
            catch (Exception e)
            {
                //Prep.LogError("string -> Serial 변환 오류 : ", e.Message, GetType());

            }

            return null;
        }

        /// <summary>
        /// 저장된 데이터 변환하기
        /// Byte -> Serial
        /// </summary>
        /// <param name="dataByte"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public StorableData DataConvertByteToSerial(byte[] data)
        {

            try
            {

                using (MemoryStream memory = new MemoryStream())
                {
                    memory.Write(data, 0, data.Length);
                    memory.Position = 0;

                    IFormatter bf = new BinaryFormatter();

                    string str = Convert.ToBase64String(memory.ToArray());
                    Debug.Log("convert : " + str);

                    StorableData accountData = (StorableData)bf.Deserialize(memory);

                    memory.Close();
                    return accountData;

                }

            }
            catch (Exception e)
            {
                //Prep.LogError("Byte -> Serial 변환 오류 : ", e.Message, GetType());
            }

            return null;
        }


        /// <summary>
        /// 마지막으로 저장한 시간 가져오기
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Nullable<DateTime> getLastWriteTime(string fileName)
        {
            if (isFile(fileName))
            {
                return TimeZoneInfo.ConvertTimeToUtc(File.GetLastWriteTime(string.Format("{0}/{1}.dat", filePath, fileName)));
            }

            return null;
        }


        #region ########## 파일 입출력 ##########

        /// <summary>
        /// 파일 입출력 저장 - Formatter
        /// 암호화 할당
        /// </summary>
        public bool SaveFileData(object data, string fileName, System.Action<TYPE_IO_RESULT> endCallback)
        {
            try
            {
                using (FileStream file = new FileStream(string.Format("{0}/{1}.txt", filePath, fileName), FileMode.Create, FileAccess.Write))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(file, tdes.CreateEncryptor(key, salt), CryptoStreamMode.Write))
                    {
                        IFormatter bf = new BinaryFormatter();
                        bf.Serialize(cryptoStream, data);
                        cryptoStream.Close();
                        file.Close();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                //Prep.LogError("파일 입출력 저장 오류 : ", e.Message, GetType());

                return false;
            }

        }

        /// <summary>
        /// 파일 입출력 저장 - Formatter
        /// 비암호화
        /// </summary>
        public bool SaveFileData_NotCrypto(object data, string fileName, System.Action<TYPE_IO_RESULT> endCallback)
        {
            try
            {
                using (FileStream file = new FileStream(string.Format("{0}/{1}.txt", filePath, fileName), FileMode.Create, FileAccess.Write))
                {
                    IFormatter bf = new BinaryFormatter();
                    bf.Serialize(file, data);
                    file.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }



        /// <summary>
        /// 파일 입출력 불러오기 - Formatter
        /// 데이터 불러오기
        /// </summary>
        /// <returns><c>true</c>, if data was loaded, <c>false</c> otherwise.</returns>
        public void LoadFileData(string fileName, Action<float> processCallback, Action<TYPE_IO_RESULT, object> endCallback)
        {
            //파일 유무 판단
            if (isFile(fileName))
            {

                try
                {

                    using (FileStream file = new FileStream(string.Format("{0}/{1}.dat", filePath, fileName), FileMode.Open, FileAccess.Read))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(file, tdes.CreateDecryptor(key, salt), CryptoStreamMode.Read))
                        {
                            IFormatter bf = new BinaryFormatter();
                            StorableData accountData = (StorableData)bf.Deserialize(cryptoStream);
                            Debug.Log("파일 불러오기 완료 : " + accountData.GetType());
                            cryptoStream.Close();
                            file.Close();
                            endCallback?.Invoke(TYPE_IO_RESULT.Success, accountData);
                        }
                    }
                }
                catch (Exception e)
                {
                    //Prep.LogError("파일 입출력 불러오기 오류 : ", e.Message, GetType());
                    //Debug.LogError("백업 파일 불러오기");
                    //return loadData(fileName + "_b");
                    endCallback?.Invoke(TYPE_IO_RESULT.DataProcessingError, null);
                }
            }
        }

        #endregion


        #region ############### 암호화 ###########################

        private string DESEncrypting(string data)
        {


            //salt = new byte[8];

            //		using (rngCsp = new RNGCryptoServiceProvider()) {
            //			rngCsp.GetBytes(salt);
            //		}

            //		int myIterations = 1000;

            try
            {
                Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(key, salt, myIterations);
                //			TripleDES encAlg = TripleDES.Create ();
                //			encAlg.Key = k1.GetBytes(16);

                MemoryStream ms = new MemoryStream();

                StreamWriter sw = new StreamWriter(new CryptoStream(ms, new RijndaelManaged().CreateEncryptor(k1.GetBytes(32), k1.GetBytes(16)), CryptoStreamMode.Write));

                //			byte[] utfd1 = new System.Text.UTF8Encoding(false).GetBytes(data);

                sw.Write(data);

                sw.Close();

                return Convert.ToBase64String(ms.ToArray());

                //			k1.Reset();
            }
            catch (Exception e)
            {
                Debug.LogError("암호화 오류 : " + e.Message);
            }

            return null;

            //		RijndaelManaged rijndaelCipher = new RijndaelManaged ();
            //
            //		//입력받은 데이터를 바이트 배열로 변환
            //		byte[] plainText = System.Text.Encoding.Unicode.GetBytes (data);
            //
            //		//딕셔너리 공격을 대비하여 키를 더 풀기 어렵게 만들기 위한 Salt 사용
            //		byte[] salt = System.Text.Encoding.ASCII.GetBytes (key.Length.ToString ());
            //
            //
            //		Rfc2898DeriveBytes secretKey = new Rfc2898DeriveBytes (key, salt);
            //
            //		//내부적인 오류로 인해 PasswordDeriveBytes는 권장하지 않음 - MS
            ////		PasswordDeriveBytes secretKey = new PasswordDeriveBytes (key, salt);
            //
            //
            //
            //		ICryptoTransform Encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
            //
            //		MemoryStream memoryStream = new MemoryStream();
            //
            //		CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
            //
            //		cryptoStream.Write(plainText, 0, plainText.Length);
            //
            //		cryptoStream.FlushFinalBlock();
            //
            //		byte[] cipherBytes = memoryStream.ToArray();
            //
            //		memoryStream.Close();
            //		cryptoStream.Close();
            //
            //		string EncrytedData = Convert.ToBase64String(cipherBytes);
            //
            //		return EncrytedData;
        }



        private string DESDecrypting(string data)
        {

            //		RijndaelManaged rijndaelCipher = new RijndaelManaged ();


            //salt = new byte[8];

            //		using (rngCsp = new RNGCryptoServiceProvider()) {
            //			rngCsp.GetBytes(salt);
            //		}

            //		int myIterations = 1000;
            //		rngCsp.Dispose ();

            try
            {

                Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(key, salt, myIterations);

                //			TripleDES decAlg = TripleDES.Create ();
                //			decAlg.Key = k1.GetBytes(16);
                //			decAlg.IV = 

                ICryptoTransform cTransform = new RijndaelManaged().CreateDecryptor(k1.GetBytes(32), k1.GetBytes(16));

                byte[] bytes = Convert.FromBase64String(data);

                return new StreamReader(new CryptoStream(new MemoryStream(bytes), cTransform, CryptoStreamMode.Read)).ReadToEnd();

                //			MemoryStream decryptionStreamBacking = new MemoryStream();
                //			CryptoStream decrypt = new CryptoStream(decryptionStreamBacking, decAlg.CreateDecryptor(), CryptoStreamMode.Write);
                //
                //			byte[] data1 = System.Text.Encoding.ASCII.GetBytes(data);
                //
                //			decrypt.Write(data1, 0, data1.Length);
                //
                //			decrypt.Flush();
                //
                //			decrypt.Close ();
                //
                //			string data2 = new System.Text.UTF8Encoding(false).GetString(decryptionStreamBacking.ToArray());
                //
                //			return data2;
                //

            }
            catch (Exception e)
            {
                Debug.LogError("복호화 오류 : " + e.Message);
            }
            return null;


            //		RijndaelManaged rijndaelCipher = new RijndaelManaged ();
            //
            //		byte[] EncryptedData = Convert.FromBase64String (data);
            //		byte[] salt = System.Text.Encoding.ASCII.GetBytes (key.Length.ToString ());
            //
            //		Rfc2898DeriveBytes secretKey = new Rfc2898DeriveBytes (key, salt);
            //
            //		ICryptoTransform Decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
            //
            //		MemoryStream memoryStream = new MemoryStream (EncryptedData);
            //
            //		CryptoStream cryptoStream = new CryptoStream (memoryStream, Decryptor, CryptoStreamMode.Read);
            //
            //		byte[] plainText = new byte[EncryptedData.Length];
            //
            //		int DecryptedCount = cryptoStream.Read (plainText, 0, plainText.Length);
            //
            //		memoryStream.Close ();
            //		cryptoStream.Close ();
            //
            //		string DecryptedData = System.Text.Encoding.Unicode.GetString (plainText, 0, DecryptedCount);
            //
            //		return DecryptedData;
        }

        #endregion

    }
    
}