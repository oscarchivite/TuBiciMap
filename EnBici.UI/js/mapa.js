//Zona de declaración de variables globales con las que mantendremos información entre llamadas de funciones
var map; //Variable para almacenar el control del mapa
var trafficLayer;
var kmlLayerCallesTranquilas;
var kmlLayerCicloCarriles;
var kmlLayerViasCiclistas;
var directionsService;
var directionsDisplay;
var madrid = new google.maps.LatLng(40.4169, -3.7033);
var markersArrayAire = [];
var markersArrayRuido = [];
var markersArrayTrafico = [];
var infowindowAire = [];
var infowindowRuido = [];
var infowindowTrafico = [];

function initMap() {
   
	//Definimos las opciones del mapa
	map = new google.maps.Map(document.getElementById('map'), {
		center: madrid, //Posicionamos como centro, la Puerta del Sol de Madrid
		zoom: 13, //Marcamos el zoom por defecto
		mapTypeId: google.maps.MapTypeId.ROADMAP, //Seleccionamos el tipo de mapa de calles
		scrollwheel: true,
		draggable: true,
		panControl: true,
		zoomControl: true,
		mapTypeControl: false, //Ocultamos el control de cambio de mapa a relieve.
		scaleControl: true,
		streetViewControl: true, //Activamos el control para simular la visualización a pie de calle del mapa
		overviewMapControl: true,
		rotateControl: true
	});

	//Inicializamos el servicio de direcciones de google maps
	directionsService = new google.maps.DirectionsService,
	directionsDisplay = new google.maps.DirectionsRenderer({
		map: map
	});

	//Definimos la capa de tráfico, que posteriormente podremos activar/desactivar
	trafficLayer = new google.maps.TrafficLayer();

	//Capa KML de tráfico de Madrid
	kmlLayerTrafico = new google.maps.KmlLayer({
	    url: 'http://informo.madrid.es/informo/tmadrid/INTENSIDADES.kml'
	});

	//Capa KML de calles tranquilas
	kmlLayerCallesTranquilas = new google.maps.KmlLayer({
		url: 'https://sites.google.com/site/tubicimap/kml/calles_tranquilas.kml'
	});
	//Capa KML de ciclo carriles
	kmlLayerCicloCarriles = new google.maps.KmlLayer({
		url: 'https://sites.google.com/site/tubicimap/kml/ciclo_carriles.kml'
	});
	//Capa KML de las vías ciclistas
	kmlLayerViasCiclistas = new google.maps.KmlLayer({
		url: 'https://sites.google.com/site/tubicimap/kml/vias_ciclistas.kml'
	});

	//Inicializamos los servicios de búsqueda de direcciones
	initAutocompleteOrigen();
	initAutocompleteDestino();   
}

//Lanzamos la función de inicializar el mapa
google.maps.event.addDomListener(window, 'load', initMap);

//Función para mostrar/ocultar los marcadores de las estaciones de medición de la calidad del aire.
function CapaCalidadAire() {

	if (markersArrayAire.length == 0) {
		$.ajax({
			type: 'POST',
			url: 'WebMethods.aspx/CrearMarcadorAire',
			dataType: 'json',
			contentType: "application/json; charset=utf-8",
			success: function (data) {
				var ltlng = [];
				for (var i = 0; i < data.d.length; i++) {
					ltlng.push(new google.maps.LatLng(data.d[i].Latitud, data.d[i].Longitud));
					var image = {
						url: data.d[i].Icono,
						size: new google.maps.Size(32, 32),
						origin: new google.maps.Point(0, 0),
						anchor: new google.maps.Point(0, 32)
					};
					var marker = new google.maps.Marker({
						map: map,
						animation: google.maps.Animation.DROP,
						icon: image,
						title: data.d[i].NombreEstacion,
						position: ltlng[i],
						zIndex: 1000
					});
					markersArrayAire.push(marker);
					infowindowAire[i] = new google.maps.InfoWindow({});
					infowindowAire[i].setContent(data.d[i].Contenido);			

					(function (marker, i) {
						google.maps.event.addListener(marker, 'click', function () {
							
							infowindowAire[i].open(map, marker);
						});
						google.maps.event.addListener(map, 'click', function () {
							infowindowAire[i].close();
						});

						google.maps.event.addListener(infowindowAire[i], 'domready', function () {

							// Reference to the DIV that wraps the bottom of infowindow
							var iwOuter = $('.gm-style-iw');

							/* Since this div is in a position prior to .gm-div style-iw.
							 * We use jQuery and create a iwBackground variable,
							 * and took advantage of the existing reference .gm-style-iw for the previous div with .prev().
							*/
							var iwBackground = iwOuter.prev();

							// Removes background shadow DIV
							iwBackground.children(':nth-child(2)').css({ 'display': 'none' });

							// Removes white background DIV
							iwBackground.children(':nth-child(4)').css({ 'display': 'none' });

							// Moves the infowindow 115px to the right.
							iwOuter.parent().parent().css({ left: '115px' });

							// Moves the shadow of the arrow 76px to the left margin.
							iwBackground.children(':nth-child(1)').attr('style', function (i, s) { return s + 'left: 76px !important;' });

							// Moves the arrow 76px to the left margin.
							iwBackground.children(':nth-child(3)').attr('style', function (i, s) { return s + 'left: 76px !important;' });

							// Changes the desired tail shadow color.
							iwBackground.children(':nth-child(3)').find('div').children().css({ 'box-shadow': 'rgba(72, 181, 233, 0.6) 0px 1px 6px', 'z-index': '1' });

							// Reference to the div that groups the close button elements.
							var iwCloseBtn = iwOuter.next();

							// Apply the desired effect to the close button
							iwCloseBtn.css({ opacity: '1', right: '38px', top: '3px', border: '7px solid #48b5e9', 'border-radius': '13px', 'box-shadow': '0 0 5px #3990B9' });

							// If the content of infowindow not exceed the set maximum height, then the gradient is removed.
							if ($('.iw-content').height() < 140) {
								$('.iw-bottom-gradient').css({ display: 'none' });
							}

							// The API automatically applies 0.7 opacity to the button after the mouseout event. This function reverses this event to the desired value.
							iwCloseBtn.mouseout(function () {
								$(this).css({ opacity: '1' });
							});
						});
					})(marker, i);
				}  
			}, 
			error: function (XMLHttpRequest, textStatus, errorThrown) { return alert('Ha ocurrido un error al recuperar los marcadores.'); }

		});
		document.getElementById('capaMarcadorAire').innerHTML = 'Ocultar calidad del aire';
		document.getElementById('capaMarcadorAire').innerText = 'Ocultar calidad del aire';
	}
	else {
		document.getElementById('capaMarcadorAire').innerHTML = 'Mostrar calidad del aire';
		document.getElementById('capaMarcadorAire').innerText = 'Mostrar calidad del aire';
		for (var i = 0; i < markersArrayAire.length; i++) {
			markersArrayAire[i].setMap(null);
		}
		markersArrayAire.length = 0;
	}    
};


function CapaNivelRuido()
{
	if (markersArrayRuido.length == 0) {
		$.ajax({
			type: 'POST',
			url: 'WebMethods.aspx/CrearMarcadorRuido',
			dataType: 'json',
			contentType: "application/json; charset=utf-8",
			success: function (data) {
				var ltlng = [];
				for (var i = 0; i < data.d.length; i++) {
					ltlng.push(new google.maps.LatLng(data.d[i].Latitud, data.d[i].Longitud));
					var image = {
						url: data.d[i].Icono,
						size: new google.maps.Size(32, 32),
						origin: new google.maps.Point(0, 0),
						anchor: new google.maps.Point(0, 32)
					};
					var marker = new google.maps.Marker({
						map: map,
						animation: google.maps.Animation.DROP,
						icon: image,
						title: data.d[i].NombreEstacion,
						position: ltlng[i]
					});

					markersArrayRuido.push(marker);
					infowindowRuido[i] = new google.maps.InfoWindow({});
					infowindowRuido[i].setContent(data.d[i].Contenido);

					(function (marker, i) {
						google.maps.event.addListener(marker, 'click', function () {

							infowindowRuido[i].open(map, marker);
						});
						google.maps.event.addListener(map, 'click', function () {
							infowindowRuido[i].close();
						});

						google.maps.event.addListener(infowindowRuido[i], 'domready', function () {

							// Reference to the DIV that wraps the bottom of infowindow
							var iwOuter = $('.gm-style-iw');

							/* Since this div is in a position prior to .gm-div style-iw.
							 * We use jQuery and create a iwBackground variable,
							 * and took advantage of the existing reference .gm-style-iw for the previous div with .prev().
							*/
							var iwBackground = iwOuter.prev();

							// Removes background shadow DIV
							iwBackground.children(':nth-child(2)').css({ 'display': 'none' });

							// Removes white background DIV
							iwBackground.children(':nth-child(4)').css({ 'display': 'none' });

							// Moves the infowindow 115px to the right.
							iwOuter.parent().parent().css({ left: '115px' });

							// Moves the shadow of the arrow 76px to the left margin.
							iwBackground.children(':nth-child(1)').attr('style', function (i, s) { return s + 'left: 76px !important;' });

							// Moves the arrow 76px to the left margin.
							iwBackground.children(':nth-child(3)').attr('style', function (i, s) { return s + 'left: 76px !important;' });

							// Changes the desired tail shadow color.
							iwBackground.children(':nth-child(3)').find('div').children().css({ 'box-shadow': 'rgba(72, 181, 233, 0.6) 0px 1px 6px', 'z-index': '1' });

							// Reference to the div that groups the close button elements.
							var iwCloseBtn = iwOuter.next();

							// Apply the desired effect to the close button
							iwCloseBtn.css({ opacity: '1', right: '38px', top: '3px', border: '7px solid #48b5e9', 'border-radius': '13px', 'box-shadow': '0 0 5px #3990B9' });

							// If the content of infowindow not exceed the set maximum height, then the gradient is removed.
							if ($('.iw-content').height() < 140) {
								$('.iw-bottom-gradient').css({ display: 'none' });
							}

							// The API automatically applies 0.7 opacity to the button after the mouseout event. This function reverses this event to the desired value.
							iwCloseBtn.mouseout(function () {
								$(this).css({ opacity: '1' });
							});
						});
					})(marker, i);
				}
			},
			error: function (XMLHttpRequest, textStatus, errorThrown) { return alert('Ha ocurrido un error al recuperar los marcadores.'); }

		});
		document.getElementById('capaMarcadorRuido').innerHTML = 'Ocultar nivel de ruido';
		document.getElementById('capaMarcadorRuido').innerText = 'Ocultar nivel de ruido';
	}
	else {
		document.getElementById('capaMarcadorRuido').innerHTML = 'Mostrar nivel de ruido';
		document.getElementById('capaMarcadorRuido').innerText = 'Mostrar nivel de ruido';
		for (var i = 0; i < markersArrayRuido.length; i++) {
			markersArrayRuido[i].setMap(null);
		}
		markersArrayRuido.length = 0;
	}	
};

//Función que inicializa el autocompletado de la posición de origen
function initAutocompleteOrigen() {
	var input = document.getElementById('posOrigen');
	var searchBox = new google.maps.places.SearchBox(input);
	map.addListener('bounds_changed', function () {
		searchBox.setBounds(map.getBounds());
	});
	searchBox.addListener('places_changed', function () {
		var places = searchBox.getPlaces();
		if (places.length == 0) { return; }
	});
}

//Función que inicializa el autocompletado de la posición de destino
function initAutocompleteDestino() {
	var input = document.getElementById('posDestino');
	var searchBox = new google.maps.places.SearchBox(input);
	map.addListener('bounds_changed', function () {
		searchBox.setBounds(map.getBounds());
	});
	searchBox.addListener('places_changed', function () {
		var places = searchBox.getPlaces();
		if (places.length == 0) { return; }
	});
}

//Función para volver a centrar el mapa en Madrid
function CentrarMapa_OnClick()
{    
	map.setCenter(madrid)
	//Colocamos el zoom inicial del mapa
	map.setZoom(12);
}

//Función que calcula la ruta entre dos puntos.
function ObtenerRuta_OnClick() {

	var start = document.getElementById('posOrigen').value;
	var end = document.getElementById('posDestino').value;
	var request = {
		origin: start,
		destination: end,
		provideRouteAlternatives: true,
		optimizeWaypoints: true,
		travelMode: google.maps.DirectionsTravelMode.BICYCLING //Elegimos el como medio de transporte la bicicleta
	};

	unitSystem: google.maps.UnitSystem.METRIC

	directionsService.route(request, function (response, status) {
		if (status == google.maps.DirectionsStatus.OK) {
			directionsDisplay.setDirections(response);
		}
	});
}

function loadKmlLayer(src, map) {
	var kmlLayer = new google.maps.KmlLayer(src, {
		suppressInfoWindows: true,
		preserveViewport: false,
		map: map
	});
	google.maps.event.addListener(kmlLayer, 'click', function (event) {
		var content = event.featureData.infoWindowHtml;
		var testimonial = document.getElementById('capture');
		testimonial.innerHTML = content;
	});
}

//Función que limpia los valores de la ruta y de las ubicaciones introducidas
function LimpiarValores_OnClick() {
	directionsDisplay.setMap(null);
	document.getElementById('posOrigen').value='';
	document.getElementById('posDestino').value = '';
	kmlLayerTrafico.setMap(null);
	document.getElementById('capaTrafico').innerHTML = 'Mostrar tráfico en tiempo real Ayuntamiento de Madrid';
	document.getElementById('capaTrafico').innerText = 'Mostrar tráfico en tiempo real Ayuntamiento de Madrid';
	trafficLayer.setMap(null);
	document.getElementById('capaTraficoGoogle').innerHTML = 'Mostrar información de tráfico de Google';
	document.getElementById('capaTraficoGoogle').innerText = 'Mostrar información de tráfico de Google';
	kmlLayerCallesTranquilas.setMap(null);
	document.getElementById('capaCallesTranquilas').innerHTML = 'Mostrar calles tranquilas para la bici';
	document.getElementById('capaCallesTranquilas').innerText = 'Mostrar calles tranquilas para la bici';
	kmlLayerCicloCarriles.setMap(null);
	document.getElementById('capaCicloCarriles').innerHTML = 'Mostrar ciclo carriles';
	document.getElementById('capaCicloCarriles').innerText = 'Mostrar ciclo carriles';
	kmlLayerViasCiclistas.setMap(null);
	document.getElementById('capaViasCiclistas').innerHTML = 'Mostrar vías ciclistas';
	document.getElementById('capaViasCiclistas').innerText = 'Mostrar vías ciclistas';
	document.getElementById('capaMarcadorAire').innerHTML = 'Mostrar calidad del aire';
	document.getElementById('capaMarcadorAire').innerText = 'Mostrar calidad del aire';
	for (var i = 0; i < markersArrayAire.length; i++) {
		markersArrayAire[i].setMap(null);
	}
	markersArrayAire.length = 0;
	document.getElementById('capaMarcadorRuido').innerHTML = 'Mostrar nivel de ruido';
	document.getElementById('capaMarcadorRuido').innerText = 'Mostrar nivel de ruido';
	for (var i = 0; i < markersArrayRuido.length; i++) {
		markersArrayRuido[i].setMap(null);
	}
	markersArrayRuido.length = 0;    
}

//Función para mostrar la capa de tráfico de Google
function MostrarCapaTraficoGoogle() {
	document.getElementById('capaTraficoGoogle').innerHTML = 'Ocultar información de tráfico de Google';
	document.getElementById('capaTraficoGoogle').innerText = 'Ocultar información de tráfico de Google';
	trafficLayer.setMap(map);
}

//Función para ocultar la capa de tráfico de Google
function QuitarCapaTraficoGoogle() {
	document.getElementById('capaTraficoGoogle').innerHTML = 'Mostrar información de tráfico de Google';
	document.getElementById('capaTraficoGoogle').innerText = 'Mostrar información de tráfico de Google';
	trafficLayer.setMap(null);
}

//Función que muestra u oculta la capa de tráfico de Google
function CapaTraficoGoogle()
{
	if (trafficLayer.map == null)
	{
		document.getElementById('capaTraficoGoogle').innerHTML = 'Ocultar información de tráfico de Google';
		document.getElementById('capaTraficoGoogle').innerText = 'Ocultar información de tráfico de Google';
		trafficLayer.setMap(map);
	}
	else
	{
		document.getElementById('capaTraficoGoogle').innerHTML = 'Mostrar información de tráfico de Google';
		document.getElementById('capaTraficoGoogle').innerText = 'Mostrar información de tráfico de Google';
		trafficLayer.setMap(null);
	}
}

//Función que muestra u oculta la capa de tráfico de madrid
function MostrarTrafico() {
	if (kmlLayerTrafico.map == null) {
		document.getElementById('capaTrafico').innerHTML = 'Ocultar tráfico en tiempo real Ayuntamiento de Madrid';
		document.getElementById('capaTrafico').innerText = 'Ocultar tráfico en tiempo real Ayuntamiento de Madrid';
		kmlLayerTrafico.setMap(map);
	}
	else {
		document.getElementById('capaTrafico').innerHTML = 'Mostrar tráfico en tiempo real Ayuntamiento de Madrid';
		document.getElementById('capaTrafico').innerText = 'Mostrar tráfico en tiempo real Ayuntamiento de Madrid';
		kmlLayerTrafico.setMap(null);
	}
}

//Función que muestra u oculta la capa de las calles tranquilas
function CapaCallesTranquilas()
{
	if (kmlLayerCallesTranquilas.map == null) {
		document.getElementById('capaCallesTranquilas').innerHTML = 'Ocultar calles tranquilas para la bici';
		document.getElementById('capaCallesTranquilas').innerText = 'Ocultar calles tranquilas para la bici';
		kmlLayerCallesTranquilas.setMap(map);
	}
	else {
		document.getElementById('capaCallesTranquilas').innerHTML = 'Mostrar calles tranquilas para la bici';
		document.getElementById('capaCallesTranquilas').innerText = 'Mostrar calles tranquilas para la bici';
		kmlLayerCallesTranquilas.setMap(null);
	}
}

//Función que muestra u oculta la capa de los ciclo carriles
function CapaCicloCarriles()
{
	if (kmlLayerCicloCarriles.map == null) {
		document.getElementById('capaCicloCarriles').innerHTML = 'Ocultar ciclo carriles';
		document.getElementById('capaCicloCarriles').innerText = 'Ocultar ciclo carriles';
		kmlLayerCicloCarriles.setMap(map);
	}
	else {
		document.getElementById('capaCicloCarriles').innerHTML = 'Mostrar ciclo carriles';
		document.getElementById('capaCicloCarriles').innerText = 'Mostrar ciclo carriles';
		kmlLayerCicloCarriles.setMap(null);
	}
}

//Función que muestra u oculta la capa de las vías ciclistas
function CapaViasCiclistas() {
	if (kmlLayerViasCiclistas.map == null) {
		document.getElementById('capaViasCiclistas').innerHTML = 'Ocultar vías ciclistas';
		document.getElementById('capaViasCiclistas').innerText = 'Ocultar vías ciclistas';
		kmlLayerViasCiclistas.setMap(map);
	}
	else {
		document.getElementById('capaViasCiclistas').innerHTML = 'Mostrar vías ciclistas';
		document.getElementById('capaViasCiclistas').innerText = 'Mostrar vías ciclistas';
		kmlLayerViasCiclistas.setMap(null);
	}
}



