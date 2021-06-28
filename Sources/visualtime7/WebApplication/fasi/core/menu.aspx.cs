using InMotionGIT.Common.Proxy;
using InMotionGIT.Common.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using System.Dynamic;

public partial class fasi_menu : System.Web.UI.Page
{
    [WebMethod()]
    public static List<object> MainMenu()
    {
        List<object> result = new List<object>();
        using (DataManagerFactory dbFactory = new DataManagerFactory("SELECT TRIM(SCODISPL) AS code, TRIM(SDESCRIPT) AS description, TRIM(SHELPPATH) AS help FROM WINDOWS WHERE SCODMEN = 'MENU' AND sStatregt = '1' ORDER BY NSEQUENCE",
                                                                     "WINDOWS", "Linked.LatCombined"))
        {
            DataTable data = dbFactory.QueryExecuteToTable(true);

            //IEnumerable<object> results = from DataRow row in data.AsEnumerable()
            //              select new
            //              {
            //                  code = row.StringValue("CODE"),
            //                  description = row.StringValue("DESCRIPTION"),
            //                  help = row.StringValue("HELP")
            //              };
            foreach (DataRow row in data.Rows)
            {
                result.Add(new
                {
                    code = row.StringValue("CODE"),
                    description = row.StringValue("DESCRIPTION"),
                    help = row.StringValue("HELP")
                });
            }
        }
        return result;
    }

     [WebMethod()]
    public static List<object> CustomMenu(int userId)
    {
        List<object> result = new List<object>();
        string filter = string.Empty;
        userId = 28579;
        using (DataManagerFactory dbFactory = new DataManagerFactory("SELECT CODISPL AS CODE FROM USERCUSTOMMENU WHERE USERID = @:USERID ORDER BY OPTIONORDER",
                                                                     "WINDOWS", "Linked.FrontOffice"))
        {
            dbFactory.AddParameter("USERID", DbType.Decimal, 9, false, userId);
            DataTable data = dbFactory.QueryExecuteToTable(true);

            foreach (DataRow row in data.Rows)
            {
                filter += string.Format("'{0}',", row.StringValue("CODE"));
            }
        }
        if (filter.IsNotEmpty())
        {
            using (DataManagerFactory dbFactory = new DataManagerFactory("SELECT TRIM(SCODISPL) AS CODE, TRIM(SDESCRIPT) AS DESCRIPTION FROM WINDOWS WHERE SCODISPL IN (" + filter + ")",
                                                             "WINDOWS", "Linked.LatCombined"))
            {
                DataTable data = dbFactory.QueryExecuteToTable(true);

                foreach (DataRow row in data.Rows)
                {
                    result.Add(new
                    {
                        code = row.StringValue("CODE"),
                        description = row.StringValue("DESCRIPTION")
                    });
                }
            }
        }
        return result;
    }

    [WebMethod()]
    public static List<object> Batchs(int userId)
    {
        List<object> result = new List<object>();
        userId = 28579;

        using (DataManagerFactory dbFactory = new DataManagerFactory("SELECT DSUBMIT, REAGENERALPKG.REABATCH_DESC(NBATCH, 1) SBATCH, " +
                                                                          " DSTART, DEND, NSTATUS, DECODE(NSTATUS, 0, 'Deshabilitado', 1, 'Habilitado', 2, 'Enviado a proceso', 3, 'En ejecución', 4, 'Termino anormal', 5, 'Termino exitoso', '***') SSTATUSDESC, " +
                                                                          " SKEY " +
                                                                      " FROM BATCH_JOB " +
                                                                     " WHERE NUSERSUBMIT = @:NUSERSUBMIT " +
                                                                     " ORDER BY DSUBMIT DESC",
                                                                     "BATCH_JOB", "Linked.LatCombined"))
        {
            dbFactory.AddParameter("NUSERSUBMIT", DbType.Decimal, 9, false, userId);
            DataTable data = dbFactory.QueryExecuteToTable(true);

            foreach (DataRow row in data.Rows)
            {
                result.Add(new
                {
                    code = row.StringValue("CODE"),
                    description = row.StringValue("DESCRIPTION"),
                    help = row.StringValue("HELP")
                });
            }
        }
        return result;
    }

}