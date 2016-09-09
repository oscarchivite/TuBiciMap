<%@ Page Title="TuBiciMap" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TuBiciMap.aspx.cs" Inherits="EnBici._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">	
	<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=places,visualizacion&key=AIzaSyDnycWatbGyK6ldFqErjFtko1yeMclNUOA"></script>
	<script type="text/javascript" src="js/mapa.js"></script>		
	<div class="embed-responsive-item">
		<div id="nav_wrapper" class="embed-responsive-item">
			<nav class="navmenu">
				<ul class="vertical">
					<li><strong>¿Qué ruta quieres tomar?</strong></li>
					<li><a></a></li>
					<li><input id="posOrigen" class="controls" type="text" placeholder="Introduce un origen" /></li>
					<li><input id="posDestino" class="controls" type="text" placeholder="Introduce un destino" /></li>
					<li><a></a></li>
					<li><a onclick="return ObtenerRuta_OnClick()">Calcular ruta</a></li>
					<li><strong>¿Qué información necesitas?</strong></li>
					<li><a></a></li>
					<li><a id="capaMarcadorRuido" onclick="CapaNivelRuido()">Mostrar niveles de ruido</a></li>
					<li><a id="capaMarcadorAire" onclick="CapaCalidadAire()">Mostrar calidad del aire</a></li>
					<li><a id="capaTrafico" onclick="MostrarTrafico()">Mostrar tráfico en tiempo real Ayuntamiento de Madrid</a></li>
                    <li><a id="capaTraficoGoogle" onclick="CapaTraficoGoogle()">Mostrar información de tráfico de Google</a></li>
					<li><a id="capaCallesTranquilas" onclick="CapaCallesTranquilas()">Mostrar calles tranquilas para la bici</a></li>
					<li><a id="capaCicloCarriles" onclick="CapaCicloCarriles()">Mostrar ciclo carriles</a></li>
					<li><a id="capaViasCiclistas" onclick="CapaViasCiclistas()">Mostrar vías ciclistas</a></li>
					<li><strong>¿Volvemos a empezar?</strong></li>
					<li><a></a></li>
					<li><a onclick="return LimpiarValores_OnClick()">Limpiar mapa</a></li>
					<li><a onclick="return CentrarMapa_OnClick()">Centrar mapa</a></li>
				</ul>
			</nav>                
		</div>
		<div id="map" class="embed-responsive-item"></div>        		
	</div>
	<div id="capture" class="embed-responsive-item"></div>		
</asp:Content>
