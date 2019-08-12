using System.Collections;
using System.Configuration;
using System.Data;
using SqlDataHelper;

namespace XMLEditor.Code
{
   public class DB
   {
      private static string strConn = ConfigurationManager.ConnectionStrings["Conn"].ToString();

        public static DataSet GetByID(int ID)
        {

            DataSet ds = new DataSet();
            object[] parameters;
            Hashtable ht = new Hashtable(0);
            ht.Add("@ID", ID);
            parameters = SqlHelperParameterCache.SetParameter(strConn, "XMLFilesDetailGetByID", ht);
            SqlHelper.FillDataset(strConn, "XMLFilesDetailGetByID", ds, null, parameters);

            //parameters = SqlHelperParameterCache.SetParameter(strConn, "......", ht);..
            //SqlHelper.FillDataset(strConn, "......", ds, null, parameters);

            return ds;
        }

    }
}