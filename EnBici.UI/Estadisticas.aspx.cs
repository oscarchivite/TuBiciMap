using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace EnBici
{
    public partial class Estadisticas : System.Web.UI.Page
    {
        /// <summary>
        /// Método para la carga inicial de la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                CargarComboHoras();
                CargarComboPeriodo();
                CargarComboAnio();
                CargarComboMes();
                CargarEstaciones();                
            }
        }
        
        #region Métodos funcionales
        /// <summary>
        /// Método para lanzar el proceso de generación de las gráficas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGenerarGráfica_Click(object sender, EventArgs e)
        {
            try
            {
                //Tenemos seleccionado la opción de calidad del aire
                if (int.Parse(rbSeleccionDatos.SelectedValue) == 0)
                {
                    if (int.Parse(rbDiarioMensual.SelectedValue) == 0)
                    {
                        if (idCalendarioDesde.SelectedDate == DateTime.MinValue || idCalendarioHasta.SelectedDate == DateTime.MinValue)
                        {
                            MensajeInformativo("Por favor, revise las fechas seleccionadas, debe seleccionar valores válidos.");
                        }
                        else
                        {
                            if (idCalendarioDesde.SelectedDate > idCalendarioHasta.SelectedDate)
                            {
                                MensajeInformativo("El año \"desde\" debe ser anterior al año \"hasta\".");
                            }
                            else
                            {
                                //Comprobamos si existe una diferencia máxima de 30 días, ya que sino, puede que sobrecarguemos la base de datos
                                TimeSpan ts = new TimeSpan();
                                DateTime fechaDesde = idCalendarioDesde.SelectedDate;
                                DateTime fechaHasta = idCalendarioHasta.SelectedDate;
                                ts = fechaHasta.Subtract(fechaDesde);
                                if (ts.Days > 30)
                                {
                                    MensajeInformativo("La diferencia entre las fechas no puede ser superior a 30 días.");
                                }
                                else
                                {
                                    GenerarGraficaAire();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (int.Parse(ddlAnioDesde.SelectedValue) < int.Parse(ddlAnioHasta.SelectedValue))
                        {
                            MensajeInformativo("La fecha \"desde\" debe ser anterior a la fecha \"hasta\".");
                        }
                        else
                        {
                            if ((int.Parse(ddlAnioDesde.SelectedValue) == int.Parse(ddlAnioHasta.SelectedValue))
                                && ((int.Parse(ddlMesDesde.SelectedValue) > int.Parse(ddlMesHasta.SelectedValue))))
                            {
                                MensajeInformativo("Para el mismo año seleccionado, el mes \"desde\" debe ser anterior al mes \"hasta\".");
                            }
                            else
                            {
                                if ((int.Parse(ddlAnioDesde.SelectedValue) == int.Parse(ddlAnioHasta.SelectedValue))
                                && ((int.Parse(ddlMesDesde.SelectedValue) == int.Parse(ddlMesHasta.SelectedValue))))
                                {
                                    MensajeInformativo("Debe seleccionar un intervalo de al menos un mes de diferencia.");
                                }
                                else
                                {
                                    GenerarGraficaAire();
                                }
                            }
                        }
                    }
                }
                //Tenemos seleccionado la opción de nivel de ruido
                else
                {
                    if (int.Parse(rbDiarioMensual.SelectedValue) == 0)
                    {
                        if (idCalendarioDesde.SelectedDate == DateTime.MinValue || idCalendarioHasta.SelectedDate == DateTime.MinValue)
                        {
                            MensajeInformativo("Por favor, revise las fechas seleccionadas, debe seleccionar valores válidos.");
                        }
                        else
                        {
                            if (idCalendarioDesde.SelectedDate > idCalendarioHasta.SelectedDate)
                            {
                                MensajeInformativo("El año \"desde\" debe ser anterior al año \"hasta\".");
                            }
                            else
                            {
                                //Comprobamos si existe una diferencia máxima de 30 días, ya que sino, puede que sobrecarguemos la base de datos
                                TimeSpan ts = new TimeSpan();
                                DateTime fechaDesde = idCalendarioDesde.SelectedDate;
                                DateTime fechaHasta = idCalendarioHasta.SelectedDate;
                                ts = fechaHasta.Subtract(fechaDesde);
                                if (ts.Days > 30)
                                {
                                    MensajeInformativo("La diferencia entre las fechas no puede ser superior a 30 días.");
                                }
                                else
                                {
                                    GenerarGraficaRuido();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (int.Parse(ddlAnioDesde.SelectedValue) < int.Parse(ddlAnioHasta.SelectedValue))
                        {
                            MensajeInformativo("La fecha \"desde\" debe ser anterior a la fecha \"hasta\".");
                        }
                        else
                        {
                            if ((int.Parse(ddlAnioDesde.SelectedValue) == int.Parse(ddlAnioHasta.SelectedValue))
                                && ((int.Parse(ddlMesDesde.SelectedValue) > int.Parse(ddlMesHasta.SelectedValue))))
                            {
                                MensajeInformativo("Para el mismo año seleccionado, el mes \"desde\" debe ser anterior al mes \"hasta\".");
                            }
                            else
                            {
                                if ((int.Parse(ddlAnioDesde.SelectedValue) == int.Parse(ddlAnioHasta.SelectedValue))
                                && ((int.Parse(ddlMesDesde.SelectedValue) == int.Parse(ddlMesHasta.SelectedValue))))
                                {
                                    MensajeInformativo("Debe seleccionar un intervalo de al menos un mes de diferencia.");
                                }
                                else
                                {
                                    GenerarGraficaRuido();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Negocio.ProcesoExterno.EscribirLogProceso(ex.Message + " - Método btnGenerarGráfica_Click()");
            }
        }

        /// <summary>
        /// Método que obtiene los datos para generar la consulta sobre los niveles de ruido
        /// </summary>
        private void GenerarGraficaRuido()
        {
            Entidades.Grafica grafica = new Entidades.Grafica();
            Entidades.Grafica grafica2 = new Entidades.Grafica();
            //Tomamos la fecha desde y hasta seleccionada por el usuario
            if (int.Parse(rbDiarioMensual.SelectedValue) == 0)
            {
                //Gráfica 1
                DateTime fechaDesde = idCalendarioDesde.SelectedDate;
                DateTime fechaHasta = idCalendarioHasta.SelectedDate;
                grafica.GRuido.DiaDesde = fechaDesde.Day;
                grafica.GRuido.MesDesde = fechaDesde.Month;
                grafica.GRuido.AnioDesde = fechaDesde.Year;
                grafica.GRuido.DiaHasta = fechaHasta.Day;
                grafica.GRuido.MesHasta = fechaHasta.Month;
                grafica.GRuido.AnioHasta = fechaHasta.Year;
                grafica.GRuido.Periodo = ddlPeriodo.SelectedValue;
                grafica.GRuido.CodEstacion = ddlEstacion.SelectedValue;
                grafica.GRuido.EsDiario = (int.Parse(rbDiarioMensual.SelectedValue) == 0) ? true : false;
                    
                DataSet ds = Negocio.Grafica.ObtenerInformacionContaminacionAcustica(grafica);
                if (ds != null)
                {
                    chGrafica.DataBindCrossTable(ds.Tables[0].AsEnumerable(), "Elemento", "Fecha", "Datos", string.Empty);
                    chGrafica.Titles.Add(ddlEstacion.SelectedItem.ToString());
                    chGrafica.ChartAreas[0].AxisX.Title = "Días";
                    chGrafica.ChartAreas[0].AxisY.Title = "Medida (dB)";

                    MarkerStyle marker = MarkerStyle.Star4;
                    foreach (Series ser in chGrafica.Series)
                    {
                        ser.ShadowOffset = 2;
                        ser.BorderWidth = 3;
                        ser.ChartType = SeriesChartType.FastLine;
                        ser.MarkerSize = 12;
                        ser.MarkerStyle = marker;
                        ser.MarkerBorderColor = Color.FromArgb(64, 64, 64);
                        ser.Font = new Font("Trebuchet MS", 12, FontStyle.Bold);
                        marker++;
                    }
                }
                else
                {
                    MensajeInformativo("La búsqueda para la gráfica 1 no ha devuelto ningún resultado. Por favor verifique los datos seleccionados.");
                }
                //Gráfica 2
                grafica2.GRuido.DiaDesde = fechaDesde.Day;
                grafica2.GRuido.MesDesde = fechaDesde.Month;
                grafica2.GRuido.AnioDesde = fechaDesde.Year;
                grafica2.GRuido.DiaHasta = fechaHasta.Day;
                grafica2.GRuido.MesHasta = fechaHasta.Month;
                grafica2.GRuido.AnioHasta = fechaHasta.Year;
                grafica2.GRuido.Periodo = ddlPeriodo.SelectedValue;
                grafica2.GRuido.CodEstacion = ddlEstacion2.SelectedValue;
                grafica2.GRuido.EsDiario = (int.Parse(rbDiarioMensual.SelectedValue) == 0) ? true : false;

                DataSet ds2 = Negocio.Grafica.ObtenerInformacionContaminacionAcustica(grafica2);
                if (ds2 != null)
                {
                    chGrafica2.DataBindCrossTable(ds2.Tables[0].AsEnumerable(), "Elemento", "Fecha", "Datos", string.Empty);
                    chGrafica2.Titles.Add(ddlEstacion2.SelectedItem.ToString());
                    chGrafica2.ChartAreas[0].AxisX.Title = "Días";
                    chGrafica2.ChartAreas[0].AxisY.Title = "Medida (dB)";

                    MarkerStyle marker = MarkerStyle.Star4;
                    foreach (Series ser in chGrafica2.Series)
                    {
                        ser.ShadowOffset = 2;
                        ser.BorderWidth = 3;
                        ser.ChartType = SeriesChartType.FastLine;
                        ser.MarkerSize = 12;
                        ser.MarkerStyle = marker;
                        ser.MarkerBorderColor = Color.FromArgb(64, 64, 64);
                        ser.Font = new Font("Trebuchet MS", 12, FontStyle.Bold);
                        marker++;
                    }
                }
                else
                {
                    MensajeInformativo("La búsqueda para la gráfica 2 no ha devuelto ningún resultado. Por favor verifique los datos seleccionados.");
                }
            }
            else
            {
                //Gráfica 1
                grafica.GRuido.MesDesde = int.Parse(ddlMesDesde.SelectedValue);
                grafica.GRuido.AnioDesde = int.Parse(ddlAnioDesde.SelectedValue);
                grafica.GRuido.MesHasta = int.Parse(ddlMesHasta.SelectedValue);
                grafica.GRuido.AnioHasta = int.Parse(ddlAnioHasta.SelectedValue);
                grafica.GRuido.CodEstacion = ddlEstacion.SelectedValue;
                grafica.GRuido.EsDiario = (int.Parse(rbDiarioMensual.SelectedValue) == 0) ? true : false;
                
                DataSet ds = Negocio.Grafica.ObtenerInformacionContaminacionAcustica(grafica);
                if (ds != null)
                {
                    chGrafica.DataBindCrossTable(ds.Tables[0].AsEnumerable(), "Elemento", "Fecha", "Datos", string.Empty);
                    chGrafica.Titles.Add(ddlEstacion.SelectedItem.ToString());
                    chGrafica.ChartAreas[0].AxisX.Title = "Meses";
                    chGrafica.ChartAreas[0].AxisY.Title = "Medida (dB)";

                    MarkerStyle marker = MarkerStyle.Star4;
                    foreach (Series ser in chGrafica.Series)
                    {
                        ser.ShadowOffset = 2;
                        ser.BorderWidth = 3;
                        ser.ChartType = SeriesChartType.FastLine;
                        ser.MarkerSize = 12;
                        ser.MarkerStyle = marker;
                        ser.MarkerBorderColor = Color.FromArgb(64, 64, 64);
                        ser.Font = new Font("Trebuchet MS", 12, FontStyle.Bold);
                        marker++;
                    }
                }
                else
                {
                    MensajeInformativo("La búsqueda para la gráfica 1 no ha devuelto ningún resultado. Por favor verifique los datos seleccionados.");
                }
                //Gráfica 2
                grafica2.GRuido.MesDesde = int.Parse(ddlMesDesde.SelectedValue);
                grafica2.GRuido.AnioDesde = int.Parse(ddlAnioDesde.SelectedValue);
                grafica2.GRuido.MesHasta = int.Parse(ddlMesHasta.SelectedValue);
                grafica2.GRuido.AnioHasta = int.Parse(ddlAnioHasta.SelectedValue);
                grafica2.GRuido.CodEstacion = ddlEstacion2.SelectedValue;
                grafica2.GRuido.EsDiario = (int.Parse(rbDiarioMensual.SelectedValue) == 0) ? true : false;

                DataSet ds2 = Negocio.Grafica.ObtenerInformacionContaminacionAcustica(grafica2);
                if (ds2 != null)
                {
                    chGrafica2.DataBindCrossTable(ds2.Tables[0].AsEnumerable(), "Elemento", "Fecha", "Datos", string.Empty);
                    chGrafica2.Titles.Add(ddlEstacion2.SelectedItem.ToString());
                    chGrafica2.ChartAreas[0].AxisX.Title = "Meses";
                    chGrafica2.ChartAreas[0].AxisY.Title = "Medida (dB)";

                    MarkerStyle marker = MarkerStyle.Star4;
                    foreach (Series ser in chGrafica2.Series)
                    {
                        ser.ShadowOffset = 2;
                        ser.BorderWidth = 3;
                        ser.ChartType = SeriesChartType.FastLine;
                        ser.MarkerSize = 12;
                        ser.MarkerStyle = marker;
                        ser.MarkerBorderColor = Color.FromArgb(64, 64, 64);
                        ser.Font = new Font("Trebuchet MS", 12, FontStyle.Bold);
                        marker++;
                    }
                }
                else
                {
                    MensajeInformativo("La búsqueda para la gráfica 2 no ha devuelto ningún resultado. Por favor verifique los datos seleccionados.");
                }
            }                                
        }

        /// <summary>
        /// Método que obtiene los datos para generar la consulta sobre calidad del aire
        /// </summary>
        private void GenerarGraficaAire()
        {
            if (int.Parse(chkElementosAire.SelectedValue.Count().ToString()) != 0 && int.Parse(chkElementosAire.SelectedValue.Count().ToString()) < 5)
            {
                Entidades.Grafica grafica = new Entidades.Grafica();
                Entidades.Grafica grafica2 = new Entidades.Grafica();
                //Tomamos la fecha desde y hasta seleccionada por el usuario
                if (int.Parse(rbDiarioMensual.SelectedValue) == 0)
                {
                    //Gráfica 1
                    DateTime fechaDesde = idCalendarioDesde.SelectedDate;
                    DateTime fechaHasta = idCalendarioHasta.SelectedDate;
                    grafica.GAire.DiaDesde = fechaDesde.Day;
                    grafica.GAire.MesDesde = fechaDesde.Month;
                    grafica.GAire.AnioDesde = fechaDesde.Year;
                    grafica.GAire.DiaHasta = fechaHasta.Day;
                    grafica.GAire.MesHasta = fechaHasta.Month;
                    grafica.GAire.AnioHasta = fechaHasta.Year;
                    grafica.GAire.EsDiario = (int.Parse(rbDiarioMensual.SelectedValue) == 0) ? true : false;
                    grafica.GAire.Hora = ddlHora.SelectedValue;
                    grafica.GAire.CodEstacion03 = ddlEstacion.SelectedValue;
                    bool first = false;

                    foreach (ListItem chk in chkElementosAire.Items)
                    {
                        if (chk.Selected)
                        {
                            if (!first)
                            {
                                first = true;
                                grafica.GAire.Elementos = chk.Value;
                            }
                            else
                            {
                                grafica.GAire.Elementos = grafica.GAire.Elementos + "," + chk.Value;
                            }
                        }
                    }
                    DataSet ds = Negocio.Grafica.ObtenerInformacionCalidadAire(grafica);
                    if (ds != null)
                    {
                        chGrafica.DataBindCrossTable(ds.Tables[0].AsEnumerable(), "Descripcion", "Fecha", grafica.GAire.Hora, string.Empty);
                        chGrafica.Titles.Add(ddlEstacion.SelectedItem.ToString());
                        chGrafica.ChartAreas[0].AxisX.Title = "Días";
                        chGrafica.ChartAreas[0].AxisY.Title = "Medida (µg/m3) ó (mg/m3)";

                        MarkerStyle marker = MarkerStyle.Star4;
                        foreach (Series ser in chGrafica.Series)
                        {
                            ser.ShadowOffset = 2;
                            ser.BorderWidth = 3;
                            ser.ChartType = SeriesChartType.FastLine;
                            ser.MarkerSize = 12;
                            ser.MarkerStyle = marker;
                            ser.MarkerBorderColor = Color.FromArgb(64, 64, 64);
                            ser.Font = new Font("Trebuchet MS", 12, FontStyle.Bold);
                            marker++;
                        }
                    }
                    else
                    {
                        MensajeInformativo("La búsqueda para la gráfica 1 no ha devuelto ningún resultado. Por favor verifique los datos seleccionados.");
                    }
                    //Gráfica 2
                    grafica2.GAire.DiaDesde = fechaDesde.Day;
                    grafica2.GAire.MesDesde = fechaDesde.Month;
                    grafica2.GAire.AnioDesde = fechaDesde.Year;
                    grafica2.GAire.DiaHasta = fechaHasta.Day;
                    grafica2.GAire.MesHasta = fechaHasta.Month;
                    grafica2.GAire.AnioHasta = fechaHasta.Year;
                    grafica2.GAire.EsDiario = (int.Parse(rbDiarioMensual.SelectedValue) == 0) ? true : false;
                    grafica2.GAire.Hora = ddlHora.SelectedValue;
                    grafica2.GAire.CodEstacion03 = ddlEstacion2.SelectedValue;
                    bool first1 = false;

                    foreach (ListItem chk in chkElementosAire.Items)
                    {
                        if (chk.Selected)
                        {
                            if (!first1)
                            {
                                first1 = true;
                                grafica2.GAire.Elementos = chk.Value;
                            }
                            else
                            {
                                grafica2.GAire.Elementos = grafica2.GAire.Elementos + "," + chk.Value;
                            }
                        }
                    }
                    DataSet ds2 = Negocio.Grafica.ObtenerInformacionCalidadAire(grafica2);
                    if (ds2 != null)
                    {
                        chGrafica2.DataBindCrossTable(ds2.Tables[0].AsEnumerable(), "Descripcion", "Fecha", grafica2.GAire.Hora, string.Empty);
                        chGrafica2.Titles.Add(ddlEstacion2.SelectedItem.ToString());
                        chGrafica2.ChartAreas[0].AxisX.Title = "Días";
                        chGrafica2.ChartAreas[0].AxisY.Title = "Medida (µg/m3) ó (mg/m3)";

                        MarkerStyle marker = MarkerStyle.Star4;
                        foreach (Series ser in chGrafica2.Series)
                        {
                            ser.ShadowOffset = 2;
                            ser.BorderWidth = 3;
                            ser.ChartType = SeriesChartType.FastLine;
                            ser.MarkerSize = 12;
                            ser.MarkerStyle = marker;
                            ser.MarkerBorderColor = Color.FromArgb(64, 64, 64);
                            ser.Font = new Font("Trebuchet MS", 12, FontStyle.Bold);
                            marker++;
                        }
                    }
                    else
                    {
                        MensajeInformativo("La búsqueda para la gráfica 2 no ha devuelto ningún resultado. Por favor verifique los datos seleccionados.");
                    }

                }
                else
                {
                    //Gráfica 1
                    grafica.GAire.MesDesde = int.Parse(ddlMesDesde.SelectedValue);
                    grafica.GAire.AnioDesde = int.Parse(ddlAnioDesde.SelectedValue);
                    grafica.GAire.MesHasta = int.Parse(ddlMesHasta.SelectedValue);
                    grafica.GAire.AnioHasta = int.Parse(ddlAnioHasta.SelectedValue);
                    grafica.GAire.EsDiario = (int.Parse(rbDiarioMensual.SelectedValue) == 0) ? true : false;
                    grafica.GAire.Hora = ddlHora.SelectedValue;
                    grafica.GAire.CodEstacion03 = ddlEstacion.SelectedValue;

                    bool first = false;
                    //Comprobamos si vamos a poner la gráfica para la calidad del aire

                    foreach (ListItem chk in chkElementosAire.Items)
                    {
                        if (chk.Selected)
                        {
                            if (!first)
                            {
                                first = true;
                                grafica.GAire.Elementos = chk.Value;
                            }
                            else
                            {
                                grafica.GAire.Elementos = grafica.GAire.Elementos + "," + chk.Value;
                            }
                        }
                    }
                    DataSet ds = Negocio.Grafica.ObtenerInformacionCalidadAire(grafica);
                    if (ds != null)
                    {
                        chGrafica.DataBindCrossTable(ds.Tables[0].AsEnumerable(), "Descripcion", "Mes", "Datos", string.Empty);
                        chGrafica.Titles.Add(ddlEstacion.SelectedItem.ToString());
                        chGrafica.ChartAreas[0].AxisX.Title = "Meses";
                        chGrafica.ChartAreas[0].AxisY.Title = "Medida (µg/m3) ó (mg/m3)";

                        MarkerStyle marker = MarkerStyle.Star4;
                        foreach (Series ser in chGrafica.Series)
                        {
                            ser.ShadowOffset = 2;
                            ser.BorderWidth = 3;
                            ser.ChartType = SeriesChartType.FastLine;
                            ser.MarkerSize = 12;
                            ser.MarkerStyle = marker;
                            ser.MarkerBorderColor = Color.FromArgb(64, 64, 64);
                            ser.Font = new Font("Trebuchet MS", 12, FontStyle.Bold);
                            marker++;
                        }
                    }
                    else
                    {
                        MensajeInformativo("La búsqueda para la gráfica 1 no ha devuelto ningún resultado. Por favor verifique los datos seleccionados.");
                    }
                    //Gráfica 2
                    grafica2.GAire.MesDesde = int.Parse(ddlMesDesde.SelectedValue);
                    grafica2.GAire.AnioDesde = int.Parse(ddlAnioDesde.SelectedValue);
                    grafica2.GAire.MesHasta = int.Parse(ddlMesHasta.SelectedValue);
                    grafica2.GAire.AnioHasta = int.Parse(ddlAnioHasta.SelectedValue);
                    grafica2.GAire.EsDiario = (int.Parse(rbDiarioMensual.SelectedValue) == 0) ? true : false;
                    grafica2.GAire.Hora = ddlHora.SelectedValue;
                    grafica2.GAire.CodEstacion03 = ddlEstacion2.SelectedValue;

                    bool first1 = false;
                    //Comprobamos si vamos a poner la gráfica para la calidad del aire

                    foreach (ListItem chk in chkElementosAire.Items)
                    {
                        if (chk.Selected)
                        {
                            if (!first1)
                            {
                                first1 = true;
                                grafica2.GAire.Elementos = chk.Value;
                            }
                            else
                            {
                                grafica2.GAire.Elementos = grafica2.GAire.Elementos + "," + chk.Value;
                            }
                        }
                    }
                    DataSet ds2 = Negocio.Grafica.ObtenerInformacionCalidadAire(grafica2);
                    if (ds2 != null)
                    {
                        chGrafica2.DataBindCrossTable(ds2.Tables[0].AsEnumerable(), "Descripcion", "Mes", "Datos", string.Empty);
                        chGrafica2.Titles.Add(ddlEstacion2.SelectedItem.ToString());
                        chGrafica2.ChartAreas[0].AxisX.Title = "Meses";
                        chGrafica2.ChartAreas[0].AxisY.Title = "Medida (µg/m3) ó (mg/m3)";

                        MarkerStyle marker = MarkerStyle.Star4;
                        foreach (Series ser in chGrafica2.Series)
                        {
                            ser.ShadowOffset = 2;
                            ser.BorderWidth = 3;
                            ser.ChartType = SeriesChartType.FastLine;
                            ser.MarkerSize = 12;
                            ser.MarkerStyle = marker;
                            ser.MarkerBorderColor = Color.FromArgb(64, 64, 64);
                            ser.Font = new Font("Trebuchet MS", 12, FontStyle.Bold);
                            marker++;
                        }
                    }
                    else
                    {
                        MensajeInformativo("La búsqueda para la gráfica 2 no ha devuelto ningún resultado. Por favor verifique los datos seleccionados.");
                    }
                }
            }
            else
            {
                if (int.Parse(chkElementosAire.SelectedValue.Count().ToString()) >= 5)
                {
                    MensajeInformativo("El número máximo de elementos de la calidad del aire que puede seleccionar es 4.");
                }
                else
                {
                    MensajeInformativo("Debe seleccionar algún elemento de la calidad del aire.");
                }
            }
        }
        #endregion

        #region Carga combos
        /// <summary>
        /// Método para cargar el combo de horas de medidas para la calidad del aire
        /// </summary>
        private void CargarComboHoras()
        {
            if (int.Parse(rbSeleccionDatos.SelectedValue) == 0)
            {
                ddlHora.DataTextField = "Horario";
                ddlHora.DataValueField = "ID";
                ddlHora.DataSource = Negocio.AireCalendario.ObtenerAireHoras().Tables[0];
                ddlHora.DataBind();
            }
        }

        /// <summary>
        /// Método para cargar el combo de horas de medidas para la calidad del aire
        /// </summary>
        private void CargarComboPeriodo()
        {
            if (int.Parse(rbSeleccionDatos.SelectedValue) == 1)
            {
                ddlPeriodo.DataTextField = "Descripcion";
                ddlPeriodo.DataValueField = "IdPeriodo";
                ddlPeriodo.DataSource = Negocio.AireCalendario.ObtenerRuidoPeriodo().Tables[0];
                ddlPeriodo.DataBind();
            }
        }

        /// <summary>
        /// Método para cargar el combo de los meses existentes para la búsqueda de datos
        /// </summary>
        private void CargarComboMes()
        {
            if (int.Parse(rbSeleccionDatos.SelectedValue) == 0)
            {
                DataSet ds = new DataSet();
                ddlMesDesde.DataTextField = "Mes";
                ddlMesDesde.DataValueField = "Mes";
                ddlMesHasta.DataTextField = "Mes";
                ddlMesHasta.DataValueField = "Mes";
                ds = Negocio.AireCalendario.ObtenerAireMeses();
                ddlMesDesde.DataSource = ds.Tables[0];
                ddlMesDesde.DataBind();
                ddlMesHasta.DataSource = ds.Tables[0];
                ddlMesHasta.DataBind();
            }
            else
            {
                DataSet ds = new DataSet();
                ddlMesDesde.DataTextField = "Mes";
                ddlMesDesde.DataValueField = "Mes";
                ddlMesHasta.DataTextField = "Mes";
                ddlMesHasta.DataValueField = "Mes";
                ds = Negocio.AireCalendario.ObtenerRuidoMeses();
                ddlMesDesde.DataSource = ds.Tables[0];
                ddlMesDesde.DataBind();
                ddlMesHasta.DataSource = ds.Tables[0]; 
                ddlMesHasta.DataBind();
            }
        }
        
        /// <summary>
        /// Método para cargar el combo de los meses existentes para la búsqueda de datos
        /// </summary>
        private void CargarComboAnio()
        {
            if (int.Parse(rbSeleccionDatos.SelectedValue) == 0)
            {
                DataSet ds = new DataSet();
                ddlAnioDesde.DataTextField = "Anio";
                ddlAnioDesde.DataValueField = "Anio";
                ddlAnioHasta.DataTextField = "Anio";
                ddlAnioHasta.DataValueField = "Anio";
                ds = Negocio.AireCalendario.ObtenerAireAnios();
                ddlAnioDesde.DataSource = ds.Tables[0];
                ddlAnioDesde.DataBind();
                ddlAnioHasta.DataSource = ds.Tables[0];
                ddlAnioHasta.DataBind();
            }
            else
            {
                DataSet ds = new DataSet();
                ddlAnioDesde.DataTextField = "Anio";
                ddlAnioDesde.DataValueField = "Anio";
                ddlAnioHasta.DataTextField = "Anio";
                ddlAnioHasta.DataValueField = "Anio";
                ds = Negocio.AireCalendario.ObtenerRuidoAnios();
                ddlAnioDesde.DataSource = ds.Tables[0];
                ddlAnioDesde.DataBind();
                ddlAnioHasta.DataSource = ds.Tables[0];
                ddlAnioHasta.DataBind();
            }
        }

        /// <summary>
        /// Método para obtener las estaciones de medida
        /// </summary>
        private void CargarEstaciones()
        {
            if (int.Parse(rbSeleccionDatos.SelectedValue) == 0)
            {
                ddlEstacion.DataTextField = "Nombre";
                ddlEstacion.DataValueField = "CodEstacion03";
                ddlEstacion.DataSource = Negocio.AireCalendario.ObtenerAireEstaciones();
                ddlEstacion.DataBind();
                ddlEstacion2.DataTextField = "Nombre";
                ddlEstacion2.DataValueField = "CodEstacion03";
                ddlEstacion2.DataSource = Negocio.AireCalendario.ObtenerAireEstaciones();
                ddlEstacion2.DataBind();
            }
            else
            {
                ddlEstacion.DataTextField = "Nombre";
                ddlEstacion.DataValueField = "idEstacion";
                ddlEstacion.DataSource = Negocio.AireCalendario.ObtenerRuidoEstaciones();
                ddlEstacion.DataBind();
                ddlEstacion2.DataTextField = "Nombre";
                ddlEstacion2.DataValueField = "idEstacion";
                ddlEstacion2.DataSource = Negocio.AireCalendario.ObtenerRuidoEstaciones();
                ddlEstacion2.DataBind();
            }
        }
               
        #endregion Carga combos

        #region Métodos cambio de valor seleccionado
        /// <summary>
        /// Método para mostrar/ocultar los controles de calidad del aire o de nivel de ruido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbSeleccionDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbSeleccionDatos.SelectedIndex == 0)
            {
                idAire.Visible = true;
                calendariosControl.Visible = true;
                calendariosCombo.Visible = false;
                rbDiarioMensual.Enabled = true;
                controlHora.Visible = rbSeleccionDatos.SelectedIndex == 0 ? true : false;
                ddlHora.Enabled = controlHora.Visible;
                controlPeriodo.Visible = rbSeleccionDatos.SelectedIndex == 1 ? true : false;
                ddlPeriodo.Enabled = controlPeriodo.Visible;
                ddlEstacion.Visible = true;
                CargarComboHoras();
                CargarComboAnio();
                CargarComboMes();
                CargarEstaciones();
            }
            else
            {
                idAire.Visible = false;
                calendariosControl.Visible = true;
                calendariosCombo.Visible = false;
                rbDiarioMensual.Enabled = true;
                controlHora.Visible = rbSeleccionDatos.SelectedIndex == 0 ? true : false;
                ddlHora.Enabled = controlHora.Visible;
                controlPeriodo.Visible = rbSeleccionDatos.SelectedIndex == 1 ? true : false;
                ddlPeriodo.Enabled = controlPeriodo.Visible;
                ddlEstacion.Visible = true;
                CargarComboPeriodo();
                CargarComboAnio();
                CargarComboMes();
                CargarEstaciones();
            }
        }

        /// <summary>
        /// Método para mostrar/ocultar los calendarios o los combos con los años y los meses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbDiarioMensual_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(rbDiarioMensual.SelectedValue)==0)
            {
                calendariosControl.Visible = true;
                calendariosCombo.Visible = false;
                if (int.Parse(rbSeleccionDatos.SelectedValue) != 0)
                {
                    controlPeriodo.Visible = true;
                }
            }
            else
            {
                calendariosControl.Visible = false;
                calendariosCombo.Visible = true;
                if (int.Parse(rbSeleccionDatos.SelectedValue)!=0)
                {
                    controlPeriodo.Visible = false;
                }
            }
        }  
                
        #endregion Métodos cambio de valor seleccionado

        #region Métodos auxiliares
        /// <summary>
        /// Método para mostrar un mensaje por pantalla
        /// </summary>
        /// <param name="mensaje"></param>
        private void MensajeInformativo(string mensaje)
        {
            string script = @"<script type='text/javascript'>alert('" + mensaje + "');</script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Mensaje informativo", script, false);
        }
        #endregion
    }
}