<%@ Page Title="Estadísticas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Estadisticas.aspx.cs" Inherits="EnBici.Estadisticas" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
	Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="controlChart" %>

<asp:Content ID="BodyContentEstadisticas" ContentPlaceHolderID="MainContent" runat="server">
	<div runat="server" class="boxEnlaces">
		<nav class="navmenuEnlaces">
		   <div class="leftEnlaces">
				<div class="contentEstadisticas">   
				   <div>
					   <h3>¿QUÉ QUIERES CONSULTAR?</h3>
					   <asp:RadioButtonList ID="rbSeleccionDatos" RepeatDirection="Vertical" runat="server" ToolTip="Selección de los datos a consultar" AutoPostBack="true" Visible="true" OnSelectedIndexChanged="rbSeleccionDatos_SelectedIndexChanged">
							<asp:ListItem Value="0" Selected="True">&nbsp&nbsp Datos sobre calidad del aire</asp:ListItem>
							<asp:ListItem Value="1">&nbsp&nbsp Datos sobre nivel de ruido</asp:ListItem>
					   </asp:RadioButtonList>
				   </div>
				   <div id="controlHora" runat="server" visible="true"> 
						<h3>SELECCIONE LA HORA DE LOS DATOS</h3>
						<asp:DropDownList ID="ddlHora" runat="server"></asp:DropDownList>		    
				   </div>
					<div id="controlPeriodo" runat="server" visible="false"> 
						<h3>SELECCIONE EL PERIODO</h3>
						<asp:DropDownList ID="ddlPeriodo" runat="server"></asp:DropDownList>		    
				   </div>
				   <div id="estacion" runat="server" visible="true"> 
						<H3>ESTACIÓN 1:</H3>
						<asp:DropDownList ID="ddlEstacion" runat="server"></asp:DropDownList>		    
                        <H3>ESTACIÓN 2:</H3>
						<asp:DropDownList ID="ddlEstacion2" runat="server"></asp:DropDownList>	
					</div>	
				</div>
			</div>
		   <div class="rightEnlaces">
				<div class="contentEstadisticas">
					<div>
						<h3>DATOS DIARIOS O MENSUALES</h3>
						<asp:RadioButtonList ID="rbDiarioMensual" RepeatDirection="Vertical" runat="server" ToolTip="Datos diarios o mensuales" AutoPostBack="true" Visible="true" OnSelectedIndexChanged="rbDiarioMensual_SelectedIndexChanged">
							<asp:ListItem Value="0" Selected="True">&nbsp&nbsp Diario</asp:ListItem>
							<asp:ListItem Value="1">&nbsp&nbsp Mensual</asp:ListItem>
						</asp:RadioButtonList>
					</div>                    	
				</div>	  
				<div runat="server" visible="true">
					<div class="contentEstadisticas">
						<h3>INTERVALO DE FECHAS</h3>
						<div id="calendariosControl" visible="true" runat="server">
							<div style="width:49%; float:left;">
								<asp:Label runat="server">Fecha desde:</asp:Label>
								<asp:Calendar ID="idCalendarioDesde" SelectionMode="Day" style="margin-top:1%;"
									ShowGridLines="True" ToolTip="Fecha desde" runat="server" AutoPostBack="false" Visible="true"></asp:Calendar>
							</div>
							<div style="width:49%; float:right;">
								<asp:Label runat="server">Fecha hasta:</asp:Label>
								<asp:Calendar ID="idCalendarioHasta"  SelectionMode="Day" style="margin-top:1%;"
									ShowGridLines="True" ToolTip="Fecha hasta" runat="server" AutoPostBack="false" Visible="true"></asp:Calendar>			
							</div>
						</div>
						<div id="calendariosCombo" visible="false" runat="server">
							<div style="width:100%">
								<asp:Label runat="server">Año desde:</asp:Label>
								<asp:DropDownList ID="ddlAnioDesde" runat="server" CssClass="ddl"></asp:DropDownList>
								<asp:Label runat="server">Mes desde:</asp:Label>
								<asp:DropDownList ID="ddlMesDesde" runat="server" CssClass="ddl"></asp:DropDownList>
							</div>
							<div style="width:100%">
								<asp:Label runat="server">Año hasta:&nbsp</asp:Label>
								<asp:DropDownList ID="ddlAnioHasta" runat="server" CssClass="ddl"></asp:DropDownList>
								<asp:Label runat="server">Mes hasta:&nbsp</asp:Label>
								<asp:DropDownList ID="ddlMesHasta" runat="server" CssClass="ddl"></asp:DropDownList>
							</div> 				        
						</div>
					</div>
				</div>        
			</div>
		</nav>
	</div>
	<nav class="navmenuEnlaces">
	<div id="idAire" runat="server" visible="true" class="contentEstadisticas" style="margin-top:2%; width:85%; margin:0 auto;"> 		
		<div class="contentEstadisticas">
			<h3>ELEMENTOS DE REFERENCIA DE LA CALIDAD DEL AIRE</h3>
			<div id="elementos" runat="server" visible="true" style="margin-top:1%;">
				<asp:CheckBoxList runat="server" Visible="true" ID="chkElementosAire" AutoPostBack="false" 
					RepeatDirection="Vertical" RepeatColumns="4" RepeatLayout="Table" CssClass="boxEnlaces" >
					<asp:ListItem Value="01">&nbsp Dióxido de Azufre</asp:ListItem>
					<asp:ListItem Value="06">&nbsp Monóxido de Carbono</asp:ListItem>
					<asp:ListItem Value="07">&nbsp Monóxido de Nitrógeno</asp:ListItem>
					<asp:ListItem Value="08">&nbsp Dióxido de Nitrógeno</asp:ListItem>
					<asp:ListItem Value="09">&nbsp Partículas < 2.5µm</asp:ListItem>
					<asp:ListItem Value="10">&nbsp Partículas < 10µm</asp:ListItem>
					<asp:ListItem Value="12">&nbsp Óxidos de Nitrógeno</asp:ListItem>
					<asp:ListItem Value="14">&nbsp Ozono</asp:ListItem>
					<asp:ListItem Value="20">&nbsp Tolueno</asp:ListItem>
					<asp:ListItem Value="30">&nbsp Benceno</asp:ListItem>
					<asp:ListItem Value="35">&nbsp Etilbenzeno</asp:ListItem>
					<asp:ListItem Value="37">&nbsp Metaxileno</asp:ListItem>
					<asp:ListItem Value="38">&nbsp Paraxileno</asp:ListItem>
					<asp:ListItem Value="39">&nbsp Ortoxileno</asp:ListItem>
					<asp:ListItem Value="42">&nbsp Hidrocarburos totales</asp:ListItem>
					<asp:ListItem Value="44">&nbsp Hidrocarburos no metánicos</asp:ListItem>            
				</asp:CheckBoxList>	
			</div>
		</div>	
	</div>	
	</nav>
	<nav class="navmenuEnlaces">
	<div style="margin-top:3%;">
		<div class="contentEstadisticas">
			<div style="width:8%; float:left;"><h3>GRÁFICA</h3></div>
			<div style="width:80%; float:left;">
                <div>
                    <asp:Chart ID="chGrafica" runat="server" Width="1280px" Height="600px">
                        <legends>
						    <asp:legend LegendStyle="Row" IsTextAutoFit="False" Docking="Bottom" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" Alignment="Center"></asp:legend>
					    </legends>
					    <borderskin skinstyle="Emboss"></borderskin>
						    <chartareas>                   
						    <asp:chartarea Name="area" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="Transparent" ShadowColor="Transparent">
								    <area3dstyle Rotation="10" perspective="10" Inclination="15" IsRightAngleAxes="False" wallwidth="0" IsClustered="False"></area3dstyle>
								    <position y="10" height="73" width="89.43796" x="4.824818"></position>
								    <axisy linecolor="64, 64, 64, 64" IsLabelAutoFit="False" IsStartedFromZero="true">
									    <labelstyle font="Trebuchet MS, 10pt, style=Bold" />
									    <majorgrid interval="Auto" linecolor="64, 64, 64, 64" />
									    <majortickmark interval="1" enabled="False" />
								    </axisy>
								    <axisx linecolor="64, 64, 64, 64" IsLabelAutoFit="False">
									    <labelstyle font="Trebuchet MS, 10pt, style=Bold" interval="1" />
									    <majorgrid interval="Auto" linecolor="64, 64, 64, 64" />
									    <majortickmark interval="1" enabled="False" />
								    </axisx>
						    </asp:chartarea>
					    </chartareas>
				    </asp:Chart>
                </div>
                <div>
			        <asp:Chart ID="chGrafica2" runat="server" Width="1280px" Height="600px">
                        <legends>
						    <asp:legend LegendStyle="Row" IsTextAutoFit="False" Docking="Bottom" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" Alignment="Center"></asp:legend>
					    </legends>
					    <borderskin skinstyle="Emboss"></borderskin>
						    <chartareas>                   
						    <asp:chartarea Name="area" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="Transparent" ShadowColor="Transparent">
								    <area3dstyle Rotation="10" perspective="10" Inclination="15" IsRightAngleAxes="False" wallwidth="0" IsClustered="False"></area3dstyle>
								    <position y="10" height="73" width="89.43796" x="4.824818"></position>
								    <axisy linecolor="64, 64, 64, 64" IsLabelAutoFit="false" IsStartedFromZero="true">
									    <labelstyle font="Trebuchet MS, 10pt, style=Bold" />
									    <majorgrid interval="Auto" linecolor="64, 64, 64, 64" />
									    <majortickmark interval="1" enabled="False" />
								    </axisy>
								    <axisx linecolor="64, 64, 64, 64" IsLabelAutoFit="false">
									    <labelstyle font="Trebuchet MS, 10pt, style=Bold" interval="1" />
									    <majorgrid interval="Auto" linecolor="64, 64, 64, 64" />
									    <majortickmark interval="1" enabled="False" />
								    </axisx>
						    </asp:chartarea>
					    </chartareas>
				    </asp:Chart>
                </div>
			</div>
			<div style="width:9%; float:left;">
				<asp:Button ID="btnGenerarGráfica" CssClass="btnEstadistica" Text="Generar gráfica" runat="server" OnClick="btnGenerarGráfica_Click"/>
			</div>
		</div>        
	</div>    
	</nav>
</asp:Content>
