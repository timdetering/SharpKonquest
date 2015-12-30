#define LZMAPRESENT

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.IO;

namespace System
{
   public class XmlConfig
    {
       public static CompressionAlgorithm defaultCompresor = CompressionAlgorithm.Deflate;
       /// <summary>
       /// It is called when the config needs to process a xml property, and returns the xml property real value
       /// </summary>
       public static event ParsePropertyHandler ParseProperty;
       public delegate object ParsePropertyHandler(XmlElement element);

       /// <summary>
       /// It is called when the config needs to save a object and process its string value
       /// </summary>
       public static event GetPropertyHandler GetPropertyValue;
       public delegate XmlElement GetPropertyHandler(object value);

        public XmlConfig()
        {
            diccionario = new Dictionary<string, object>(0);
            imageFormat = System.Drawing.Imaging.ImageFormat.Png;
            comprimirArchivo = true;
            compresor = defaultCompresor;
            versionPrograma = null;
        }

       public XmlConfig(string path)
            : this()
        {
            rutaArchivo = path;

            Load();
        }

       public XmlConfig(string path,bool compress):this(path)
       {
           this.Compress= compress;
       }

       public enum CompressionAlgorithm
       {
           Deflate,
           Gzip,
           LZMA
       }

        //Campos privados
        private string rutaArchivo;
        private bool comprimirArchivo;
        private Dictionary<string, object> diccionario;
        private bool autoGuardar;
        private System.Drawing.Imaging.ImageFormat imageFormat;
       private CompressionAlgorithm compresor;
       private string versionPrograma;

        #region Propiedades

       /// <summary>
       /// Obtiene o establece el compresor que se utiliza para comprimir el archivo de configuracion
       /// </summary>
       public CompressionAlgorithm Compressor
       {
           get { return compresor; }
           set { compresor = value; }
       }

        public bool Compress
        {
            get { return comprimirArchivo; }
            set { comprimirArchivo = value; }
        }

        /// <summary>
        /// Especifica si se autoguarda al establecer un campo
        /// </summary>
        public bool AutoSave
        {
            get { return autoGuardar; }
            set { autoGuardar = value; }
        }

        /// <summary>
        /// Obtiene o establece una propiedad
        /// </summary>
        public object this[string key]
        {
            get { return this.GetProperty(key); }
            set
            {
                this.SetProperty(key, value);
            }
        }

        /// <summary>
        /// Formato usado para guardar las imagenes que se establezcan. Por defecto, PNG
        /// </summary>
        public System.Drawing.Imaging.ImageFormat ImageFormat
        {
            get { return imageFormat; }
            set { imageFormat = value; }
        }

       /// <summary>
       /// Version del programa usada para crear el archivo
       /// </summary>
       public string ProgramVersion
       {
           get { return versionPrograma; }
           set { versionPrograma = value; }
       }

       /// <summary>
       /// Obtiene el numero de claves almacenadas en el diccionario
       /// </summary>
       public int Count
       { get { return diccionario.Count; } }
       
        #endregion

        #region Funciones

        public void Save()
        {
            Save(rutaArchivo);
        }

        public void Load()
        {
            Load(rutaArchivo);
        }

        public object GetProperty(string key, object defaultValue)
        {
            if (diccionario.ContainsKey(key))
                return diccionario[key];
            else
                return defaultValue;
        }

        public object GetProperty(string key)
        {
            return GetProperty(key, null);
        }

        public bool ContainsKey(string key)
        {
            return diccionario.ContainsKey(key);
        }

        #region Get values

        public bool GetBool(string key)
        {
            return (bool)this.GetProperty(key);
        }

        public bool GetBool(string key, bool defaultvalue)
        {
            return (bool)this.GetProperty(key, defaultvalue);
        }
        public char GetChar(string key)
        {
            return (char)this.GetProperty(key);
        }

        public char GetChar(string key, char defaultvalue)
        {
            return (char)this.GetProperty(key, defaultvalue);
        }

        public byte GetByte(string key)
        {
            return (byte)this.GetProperty(key);
        }

        public byte GetByte(string key, byte defaultvalue)
        {
            return (byte)this.GetProperty(key, defaultvalue);
        }

        public byte[] GetByteArray(string key)
        {
            return (byte[])this.GetProperty(key);
        }

        public byte[] GetByteArray(string key, byte[] defaultvalue)
        {
            return (byte[])this.GetProperty(key, defaultvalue);
        }

        public double GetDouble(string key)
        {
            return (double)this.GetProperty(key);
        }

        public double GetDouble(string key, double defaultvalue)
        {
            return (double)this.GetProperty(key, defaultvalue);
        }

        public Enum GetEnum(string key, Enum defaultvalue)
        {
            Enum res = (Enum)Enum.Parse(defaultvalue.GetType(), this.GetProperty(key, defaultvalue).ToString());
            return res;
        }

        public float GetFloat(string key)
        {
            return (float)this.GetProperty(key);
        }

        public float GetFloat(string key, float defaultvalue)
        {
            return (float)this.GetProperty(key, defaultvalue);
        }

        public int GetInt(string key)
        {
            return (int)this.GetProperty(key);
        }

        public int GetInt(string key, int defaultvalue)
        {
            return (int)this.GetProperty(key, defaultvalue);
        }

        public System.Collections.IList GetIList(string key)
        {
            return (System.Collections.IList)this.GetProperty(key);
        }

        public System.Collections.IList GetIList(string key, System.Collections.IList defaultvalue)
        {
            return (System.Collections.IList)this.GetProperty(key, defaultvalue);
        }

        public long GetLong(string key)
        {
            return (long)this.GetProperty(key);
        }

        public long GetLong(string key, long defaultvalue)
        {
            return (long)this.GetProperty(key, defaultvalue);
        }
        public short GetShort(string key)
        {
            return (short)this.GetProperty(key);
        }

        public short GetShort(string key, short defaultvalue)
        {
            return (short)this.GetProperty(key, defaultvalue);
        }

        public string GetString(string key)
        {
            if (this.GetProperty(key) == null)
                return null;
            else
                return this.GetProperty(key).ToString();
        }

        public string GetString(string key, string defaultvalue)
        {
            object objeto=this.GetProperty(key, defaultvalue);
            if (objeto != null)
                return objeto.ToString();
            else return null;
        }

        public string[] GetStringArray(string key)
        {
            return (string[])this.GetProperty(key);
        }

        public string[] GetStringArray(string key, string[] defaultvalue)
        {
            return (string[])this.GetProperty(key, defaultvalue);
        }

        public uint GetUInt(string key)
        {
            object obj1 = this.GetProperty(key);
            if (obj1.GetType() == typeof(uint))
            {
                return (uint)obj1;
            }
            return (uint)((int)obj1);
        }

        public uint GetUInt(string key, uint defaultvalue)
        {
            return (uint)((int)this.GetProperty(key, defaultvalue));
        }

        public ulong GetULong(string key)
        {
            return (ulong)this.GetProperty(key);
        }

        public ulong GetULong(string key, ulong defaultvalue)
        {
            return (ulong)this.GetProperty(key, defaultvalue);
        }

        public ushort GetUShort(string key)
        {
            return (ushort)this.GetProperty(key);
        }

        public ushort GetUShort(string key, ushort defaultvalue)
        {
            return (ushort)this.GetProperty(key, defaultvalue);
        }


        public System.Drawing.Color GetColor(string key)
        {
            return (System.Drawing.Color)this.GetProperty(key);
        }

        public System.Drawing.Color GetColor(string key, System.Drawing.Color defaultvalue)
        {
            return (System.Drawing.Color)this.GetProperty(key, defaultvalue);
        }


        public System.Drawing.Font GetFont(string key)
        {
            return (System.Drawing.Font)this.GetProperty(key);
        }

        public System.Drawing.Font GetFont(string key, System.Drawing.Font defaultvalue)
        {
            return (System.Drawing.Font)this.GetProperty(key, defaultvalue);
        }


        public System.Drawing.Size GetSize(string key)
        {
            return (System.Drawing.Size)this.GetProperty(key);
        }

        public System.Drawing.Size GetSize(string key, System.Drawing.Size defaultvalue)
        {
            return (System.Drawing.Size)this.GetProperty(key, defaultvalue);
        }


        public System.Drawing.Point GetPoint(string key)
        {
            return (System.Drawing.Point)this.GetProperty(key);
        }

        public System.Drawing.Point GetPoint(string key, System.Drawing.Point defaultvalue)
        {
            return (System.Drawing.Point)this.GetProperty(key, defaultvalue);
        }

        public System.Drawing.Rectangle GetRectangle(string key)
        {
            return (System.Drawing.Rectangle)this.GetProperty(key);
        }

        public System.Drawing.Rectangle GetRectangle(string key, System.Drawing.Rectangle defaultvalue)
        {
            return (System.Drawing.Rectangle)this.GetProperty(key, defaultvalue);
        }



        public System.DateTime GetDateTime(string key)
        {
            return (System.DateTime)this.GetProperty(key);
        }

        public System.DateTime GetDateTime(string key, System.DateTime defaultvalue)
        {
            return (System.DateTime)this.GetProperty(key, defaultvalue);
        }


        public System.TimeSpan GetTimeSpan(string key)
        {
            return (System.TimeSpan)this.GetProperty(key);
        }

        public System.TimeSpan GetTimeSpan(string key, System.TimeSpan defaultvalue)
        {
            return (System.TimeSpan)this.GetProperty(key, defaultvalue);
        }

        public Image GetImage(string key)
        {
            return (Image)this.GetProperty(key);
        }

        public Image GetImage(string key, Image defaultvalue)
        {
            return (Image)this.GetProperty(key, defaultvalue);
        }

        public Object[] GetArray(string key)
        {
            return ((Object[])this.GetProperty(key));
        }

        public Object[] GetArray(string key, Object[] defaultvalue)
        {
            return (Object[])this.GetProperty(key, defaultvalue);
        }

        #endregion

        public void SetProperty(string key, object value)
        {
            if (diccionario.ContainsKey(key))
                diccionario.Remove(key);
            diccionario.Add(key, value);
            if (autoGuardar == true && string.IsNullOrEmpty(rutaArchivo) == false)
                Save();
        }

        /// <summary>
        /// Elimina una propiedad especifica
        /// </summary>
        /// <param name="key">Nombre de la propiedad</param>
        public void DeleteProperty(string key)
        {
            if (diccionario.ContainsKey(key))
                diccionario.Remove(key);
            if (autoGuardar == true && string.IsNullOrEmpty(rutaArchivo) == false)
                Save();
        }
        #endregion

        public void Save(string path)
        {
            try
            {
                XmlDocument documento = new XmlDocument();

                documento.LoadXml(ObtenerCabeceraXml());

                XmlElement propiedades = documento.CreateElement("properties");
                foreach (KeyValuePair<string, object> entrada in diccionario)
                {

                    if (entrada.Value is XmlElement)
                        propiedades.AppendChild(documento.ImportNode((XmlElement)entrada.Value, true));
                    else
                        propiedades.AppendChild(EntryToXML(entrada, documento, null));
                }
                if (comprimirArchivo)
                {
                    documento.DocumentElement.AppendChild(comprimirDatos(propiedades,compresor));
                }
                else
                    documento.DocumentElement.AppendChild(propiedades);
                documento.Save(path);
            }
            catch { }
            finally
            {
                System.GC.Collect();
            }
        }

       private string ObtenerCabeceraXml()
       {
           string res = @"<?xml version=""1.0""?><XmlConfig";

           if (string.IsNullOrEmpty(versionPrograma) == false)
               res += string.Format(" programVersion=\"{0}\"", versionPrograma);
           if (comprimirArchivo)
               res += string.Format(@" compressed=""true"" algorithm=""{0}""", compresor.ToString());

           res += "/>";
           return res;
       }

       private static string LeerTexto(string archivo)
       {
           System.IO.StreamReader fichero = new System.IO.StreamReader(archivo, System.Text.Encoding.Default);
           string res = fichero.ReadToEnd();
           fichero.Close();
           return res;
       }

       public bool Load(string path)
       {
           bool loaded = false;
           try
           {
               XmlDocument documento = new XmlDocument();

                   documento.LoadXml(LeerTexto(path));
               XmlElement elementoBase;
               if (esDocumentoComprimido(documento))
               {
                   elementoBase = descomprimirDatos(documento.DocumentElement["properties"],
                       ObtenerAlgoritmo(documento));
               }
               else
               {
                   elementoBase = documento.DocumentElement["properties"];
               }

               if (elementoBase != null)
               {
                   diccionario = new Dictionary<string, object>(elementoBase.ChildNodes.Count);
                   SetValueFromXmlElement(elementoBase);
               }
               loaded = true;
           }
           catch
           {
               loaded= false;
           }
           finally
           {
               System.GC.Collect();               
           }
           return loaded;
       }

       private XmlNode comprimirDatos(XmlElement propiedades,CompressionAlgorithm compresor)
       {
           string valor = propiedades.OuterXml;

           System.IO.MemoryStream stream = new MemoryStream(valor.Length);
           System.IO.Stream streamCompresor=null;

           switch (compresor)
           {
               case CompressionAlgorithm.Deflate:
                   streamCompresor = new System.IO.Compression.DeflateStream(stream, System.IO.Compression.CompressionMode.Compress);
                   break;
               case CompressionAlgorithm.Gzip:
                   streamCompresor = new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Compress);
                   break;
#if LZMAPRESENT
               case CompressionAlgorithm.LZMA:
                   streamCompresor = new System.IO.Compression.LzmaStream(stream, System.IO.Compression.CompressionMode.Compress);
                   break;
#endif
               default:
                   streamCompresor = new System.IO.Compression.DeflateStream(stream, System.IO.Compression.CompressionMode.Compress);
                   break;
           }
           

           byte[] datos = System.Text.Encoding.Default.GetBytes(valor);
           streamCompresor.Write(datos, 0, datos.Length);
           streamCompresor.Close();

           byte[] datosComprimidos = stream.ToArray();
           stream.Close();

           //  XmlElement propiedades = documento.CreateElement("properties");
           propiedades.InnerText = Convert.ToBase64String(datosComprimidos);

           return propiedades;
       }

       private XmlElement descomprimirDatos(XmlElement propiedades, CompressionAlgorithm descompresor)
       {
           string textoComprimido = propiedades.InnerText;
           byte[] datosComprimidos = Convert.FromBase64String(textoComprimido);

           System.IO.MemoryStream stream = new MemoryStream(datosComprimidos);
           System.IO.Stream streamDescompresor = null;

           switch (descompresor)
           {
               case CompressionAlgorithm.Deflate:
                   streamDescompresor = new System.IO.Compression.DeflateStream(stream, System.IO.Compression.CompressionMode.Decompress);
                   break;
               case CompressionAlgorithm.Gzip:
                   streamDescompresor = new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress);
                   break;
#if LZMAPRESENT
               case CompressionAlgorithm.LZMA:
                   streamDescompresor = new System.IO.Compression.LzmaStream(stream, System.IO.Compression.CompressionMode.Decompress);
                   break;
#endif
               default: 
                   streamDescompresor = new System.IO.Compression.DeflateStream(stream, System.IO.Compression.CompressionMode.Decompress);
                                    break;
           }

           int contador = 1;
           byte[] datosLeidos;
           int leido;
           do
           {
               datosLeidos = new byte[2097152 * contador];
               leido = streamDescompresor.Read(datosLeidos, 0, datosLeidos.Length);
               contador++;
           } while (leido == datosLeidos.Length - 1);


           string textoDescomprimido = System.Text.Encoding.Default.GetString(datosLeidos, 0, leido);

           propiedades.InnerXml = textoDescomprimido;
           return propiedades["properties"];
       }

       private CompressionAlgorithm ObtenerAlgoritmo(XmlDocument documento)
       {
           XmlAttribute atributo = documento.DocumentElement.Attributes["algorithm"];
           if (atributo != null)
           {
               return (CompressionAlgorithm)Enum.Parse(typeof(CompressionAlgorithm),atributo.Value);
           }
           else
           {
               return CompressionAlgorithm.Deflate;
           }
       }

       private bool esDocumentoComprimido(XmlDocument documento)
       {
           XmlAttribute atributo=  documento.DocumentElement.Attributes["compressed"];
           if(atributo!=null && string.Compare(atributo.Value,"true",true)==0)
               return true;
           else
               return false;
       }

        #region Convertir los valores a XML

        private XmlElement EntryToXML(KeyValuePair<string, object> entrada, XmlDocument doc, string nombreEntrada)
        {
            XmlElement propiedadActual;

            if (nombreEntrada == null || nombreEntrada == string.Empty)
                propiedadActual = doc.CreateElement("property");
            else
                propiedadActual = doc.CreateElement(nombreEntrada);

            if (entrada.Key != null && (string)entrada.Key != string.Empty)
            {
                XmlAttribute atributoKey = doc.CreateAttribute("key");
                atributoKey.InnerText = entrada.Key.ToString();
                propiedadActual.Attributes.Append(atributoKey);
            }

            string TypeName = null;
            string datosEntrada;
            if (entrada.Value == null)
            {
                TypeName = "Null";
                datosEntrada = null;
            }
            else
                datosEntrada = PropertyToXML(entrada.Value, propiedadActual, doc, out TypeName);

            XmlAttribute atributoTipo = doc.CreateAttribute("type");
            if (TypeName != null)
                atributoTipo.InnerText = TypeName;
            else
            {
                if (entrada.Value is Enum)
                    atributoTipo.InnerText = "Enum";
                else
                    atributoTipo.InnerText = entrada.Value.GetType().Name;
            }
            propiedadActual.Attributes.Append(atributoTipo);

            if (datosEntrada != null && datosEntrada != "")
            {
                XmlAttribute atributoValue = doc.CreateAttribute("value");
                atributoValue.InnerText = datosEntrada;
                propiedadActual.Attributes.Append(atributoValue);
            }

            return propiedadActual;
        }


        /// <summary>
        /// Convierte un objeto a cadena
        /// </summary>
        public string ConvertToString(object objectValue)
        {
            if (objectValue == null)
                return null;
            if (objectValue is byte || objectValue is int || objectValue is uint
                || objectValue is bool || objectValue is short || objectValue is ushort
                || objectValue is float || objectValue is long || objectValue is ulong
                || objectValue is double || objectValue is System.DateTime || objectValue is System.TimeSpan
                || objectValue is string || objectValue is Enum || objectValue is decimal
                || objectValue is System.Single || objectValue is System.Char)
            {
                return objectValue.ToString();
            }
            if (objectValue is byte[])
            {
                return Convert.ToBase64String((byte[])objectValue);
            }
            if (objectValue is System.Drawing.Color)
            {
                System.Drawing.Color color = (System.Drawing.Color)objectValue;
                return color.ToArgb().ToString("X");
            }

            if (objectValue is System.Drawing.Size)
            {
                System.Drawing.Size size = (System.Drawing.Size)objectValue;
                return size.Width + " , " + size.Height;
            }

            if (objectValue is System.Drawing.Point)
            {
                System.Drawing.Point point = (System.Drawing.Point)objectValue;
                return point.X + " , " + point.Y;
            }

            if (objectValue is System.Drawing.Font)
            {
                System.Drawing.Font fuente = (System.Drawing.Font)objectValue;
                return string.Format("{0} , {1}", fuente.Name, fuente.Size.ToString().Replace(',', '.'));
            }
            if (objectValue is Icon)
            {
                Icon icono = (Icon)objectValue;
                System.IO.MemoryStream streamIcono = new MemoryStream(10240);
                icono.Save(streamIcono);
                string strIcono = System.Convert.ToBase64String(streamIcono.ToArray());
                streamIcono.Close();

                return strIcono;
            }
            if (objectValue is System.IO.Stream)
            {
                System.IO.Stream stream = (System.IO.Stream)objectValue;
                byte[] buffer = new byte[(int)stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
                string strCadena = Convert.ToBase64String((byte[])buffer);
                buffer = null;
                return strCadena;
            }
            return null;
        }


        /// <returns>
        /// Devuelve el valor del atributo value
        /// </returns>
        private string PropertyToXML(object EntryValue, XmlElement propiedad, XmlDocument doc, out string TypeName)
        {
            TypeName = null;

            if (GetPropertyValue != null)
            {
                XmlElement xml=GetPropertyValue(EntryValue);
                if (xml != null)
                {
                    propiedad.AppendChild(xml);
                    return null;
                }
            }

            #region Tipos complejos

            if (EntryValue is System.Drawing.Rectangle)
            {
                System.Drawing.Rectangle rectangulo = (System.Drawing.Rectangle)EntryValue;

                propiedad.AppendChild(EntryToXML(
                    new KeyValuePair<string, object>(string.Empty, rectangulo.Size), doc, "Size"));

                propiedad.AppendChild(EntryToXML(
                    new KeyValuePair<string, object>(string.Empty, rectangulo.Location), doc, "Location"));

                TypeName = "Rectangle";
                return null;
            }
            if (EntryValue is string && ((string)EntryValue).Length > 250)
            {
                //CDATA
                XmlCDataSection cdata = doc.CreateCDataSection(EntryValue.ToString());
                propiedad.AppendChild(cdata);
                return null; 
            }
            if (EntryValue is string[])
            {
                string[] textArray1 = (string[])EntryValue;
                for (int num1 = 0; num1 < textArray1.Length; num1++)
                {
                    string textoActual = textArray1[num1];
                    XmlElement element3 = doc.CreateElement("entry");
                    element3.InnerText = textoActual;
                    propiedad.AppendChild(element3);
                }
                TypeName = "String[]";
                return null;
            }
            if (EntryValue is Image)
            {
                Image imagen = (Image)EntryValue;

                XmlAttribute atributoFormato = doc.CreateAttribute("format");
                atributoFormato.InnerText = this.imageFormat.ToString();
                propiedad.Attributes.Append(atributoFormato);

                System.IO.MemoryStream streamImage = new MemoryStream(10240);
                imagen.Save(streamImage, imageFormat);
                string strImage = System.Convert.ToBase64String(streamImage.ToArray());
                streamImage.Close();
                TypeName = "Image";
                return strImage;
            }
            if (EntryValue is Array)
            {
                System.Array array = (System.Array)EntryValue;
                foreach (object actual in array)
                {
                    propiedad.AppendChild(
                        EntryToXML(
                        new KeyValuePair<string, object>(string.Empty, actual),
                        doc
                        , "entry"
                        ));
                }
                TypeName = "Array";
                return null;
            }
            if (EntryValue is System.Collections.IList)
            {
                System.Collections.IList list = (System.Collections.IList)EntryValue;
                foreach (object actual in list)
                {
                    propiedad.AppendChild(EntryToXML(new KeyValuePair<string, object>(string.Empty, actual), doc
                        , "entry"));
                }
                TypeName = "IList";
                return null;
            }
            #endregion

            return this.ConvertToString(EntryValue);
        }

       protected void SetValueFromXmlElement(XmlElement element)
       {
           if (element == null || element.ChildNodes == null)
               return;

           foreach (XmlElement element1 in element.ChildNodes)
           {
               if (element1.Name != "property")
               {
                   continue;
               }
               try
               {
                   diccionario[element1.Attributes["key"].InnerText] =
                       GetValueFromXmlElement(element1);
               }
               catch (Exception)
               {
                   continue;
               }
           }
       }

       /// <summary>
       /// Genera una varible a partir del atributo value
       /// </summary>
       protected object GetValueFromXmlElement(XmlElement element)
       {
           string Tipo = element.Attributes["type"].InnerText;

           if (ParseProperty != null)
               return ParseProperty(element);

           #region Tipos complejos
           switch (Tipo)
           {
               case "Rectangle":
                   {
                       if (!element.HasChildNodes)
                       {
                           return null;
                       }
                       System.Drawing.Rectangle rectangulo = new Rectangle(0, 0, 0, 0);

                       rectangulo.Size = (Size)GetValueFromXmlElement(element["Size"]);
                       rectangulo.Location = (Point)GetValueFromXmlElement(element["Location"]);

                       return rectangulo;
                   }
               case "String":
                   {
                       if (!element.HasChildNodes)
                           return element.Attributes["value"].InnerText;
                       else
                          return element.ChildNodes[0].Value;
                   }
               case "String[]":
                   {
                       if (!element.HasChildNodes)
                       {
                           return null;
                       }
                       string[] textArray1 = new string[element.ChildNodes.Count];
                       int num1 = 0;
                       foreach (XmlElement element2 in element)
                       {
                           textArray1[num1] = element2.InnerText;
                           num1++;
                       }
                       return textArray1;
                   }
               case "Array":
                   {
                       if (!element.HasChildNodes)
                       {
                           return null;
                       }
                       object[] array = new object[element.ChildNodes.Count];
                       int num1 = 0;
                       foreach (XmlElement element2 in element)
                       {
                           array[num1] = GetValueFromXmlElement(element2);
                           num1++;
                       }
                       return array;
                   }
               case "IList":
                   {
                       if (!element.HasChildNodes)
                       {
                           return null;
                       }
                       System.Collections.ArrayList list = new System.Collections.ArrayList(element.ChildNodes.Count);
                       foreach (XmlElement element2 in element)
                       {
                           list.Add(GetValueFromXmlElement(element2));
                       }
                       return list;
                   }
           }
           #endregion

           if (element.Attributes["value"] == null)
               return null;

           string valueText = element.Attributes["value"].InnerText;
           return ParseString(valueText, Tipo);
       }

       /// <summary>
       /// Convierte un texto a un objeto especificado compatible con la clase actual
       /// </summary>
       /// <param name="value"></param>
       /// <param name="type"></param>
       public object ParseString(string textValue, string desType)
       {
           switch (desType)
           {
               case "String":
                   {
                       return textValue;
                   }
               case "Int32":
                   {
                       return int.Parse(textValue);
                   }
               case "byte":
                   {
                       return byte.Parse(textValue);
                   }
               case "Char":
                   {
                       return char.Parse(textValue);
                   }
               case "UInt32":
                   {
                       return uint.Parse(textValue);
                   }
               case "Bool":
                   {
                       return bool.Parse(textValue);
                   }
               case "Boolean":
                   {
                       return bool.Parse(textValue);
                   }
               case "Short":
                   {
                       return short.Parse(textValue);
                   }
               case "UShort":
                   {
                       return ushort.Parse(textValue);
                   }
               case "Float":
                   {
                       return float.Parse(textValue.Replace('.', ','));
                   }
               case "Long":
                   {
                       return long.Parse(textValue);
                   }
               case "ULong":
                   {
                       return ulong.Parse(textValue);
                   }
               case "Double":
                   {
                       return double.Parse(textValue);
                   }
               case "Decimal":
                   {
                       return decimal.Parse(textValue);
                   }
               case "Single":
                   {
                       return Single.Parse(textValue);
                   }
               case "Byte[]":
                   {
                       byte[] buffer = Convert.FromBase64String(textValue);
                       return buffer;
                   }
               case "Enum":
                   {
                       return textValue;
                   }
               case "Color":
                   {
                       return System.Drawing.Color.FromArgb(int.Parse(textValue, System.Globalization.NumberStyles.HexNumber));
                   }
               case "Point":
                   {
                       string[] partes = textValue.Split(',');
                       return
                           new System.Drawing.Point(
                           int.Parse(partes[0]), int.Parse(partes[1]));
                   }
               case "Size":
                   {
                       string[] partes = textValue.Split(',');
                       return
                           new System.Drawing.Size(
                           int.Parse(partes[0]), int.Parse(partes[1]));
                   }

               case "Font":
                   {
                       string[] partes = textValue.Split(',');
                       return
                           new System.Drawing.Font(partes[0].Trim(),
                           float.Parse(partes[1].Replace('.', ',')));
                   }

               case "DateTime":
                   {
                       return System.DateTime.Parse(textValue);
                   }

               case "TimeSpan":
                   {
                       return System.TimeSpan.Parse(textValue);
                   }
               case "Image":
                   {
                       Image imagen = null;
                       System.IO.MemoryStream streamImage = new MemoryStream(10240);
                       byte[] buffer = Convert.FromBase64String(textValue);

                       streamImage.Write(buffer, 0, buffer.Length);

                       streamImage.Position = 0;
                       imagen = Image.FromStream(streamImage);

                       streamImage.Close();
                       buffer = null;
                       return imagen;
                   }

               case "Icon":
                   {
                       Icon icono = null;
                       System.IO.MemoryStream streamIcono = new MemoryStream(textValue.Length);
                       byte[] buffer = Convert.FromBase64String(textValue);

                       streamIcono.Write(buffer, 0, buffer.Length);

                       streamIcono.Position = 0;
                       icono = new Icon(streamIcono);

                       streamIcono.Close();
                       buffer = null;
                       return icono;
                   }
               case "Null":
                   {
                       return null;
                   }
               default:
                   {
                       return null;
                   }
           }
       }

#endregion
    }
}