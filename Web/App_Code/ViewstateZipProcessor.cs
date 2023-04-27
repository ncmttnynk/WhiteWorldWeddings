using System.IO;
using Zip = ICSharpCode.SharpZipLib.Zip.Compression;
/// <summary>
/// Summary description for ViewstateZip
/// </summary>
public class ViewstateZipProcessor
{
    public static byte[] Compress(byte[] bytes)
    {
        MemoryStream memory = new MemoryStream();
        Zip.Streams.DeflaterOutputStream stream = 
            new Zip.Streams.DeflaterOutputStream(memory, new Zip.Deflater(Zip.Deflater.BEST_COMPRESSION), 131072);
        stream.Write(bytes, 0, bytes.Length);
        stream.Close();
        return memory.ToArray();
    }

    public static byte[] Decompress(byte[] bytes)
    {
        Zip.Streams.InflaterInputStream stream = 
            new Zip.Streams.InflaterInputStream(new MemoryStream(bytes));
        MemoryStream memory = new MemoryStream();
        byte[] writeData = new byte[4096];
        int size;
        while (true)
        {
            size = stream.Read(writeData, 0, writeData.Length);
            if (size > 0)
            {
                memory.Write(writeData, 0, size);
            }
            else
            {
                break;
            }
        }
        stream.Close();
        return memory.ToArray();
    }

}