<%@ Page Title="Sobre nosotros" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="EnBici.About" %>

<asp:Content ID="BodyContentAbaut" ContentPlaceHolderID="MainContent" runat="server">
    <h3 style="text-align:center; margin-top:2%;">Proyecto de fin de grado de Ingeniería Informática</h3>
    <div class="box">
        <div class="left">            
            <div class="contentTXT">
                <img src="img/ebike.jpg" alt="Bicicleta en el campo" style="max-width:100%;"/>
            </div>
        </div>
        <div class="right">
            <div class="contentTXT">
        Esta aplicación y su documentación asociada, constituyen el trabajo de fin de grado en Ingeniería Informática. 
        Este proyecto se ha basado en el concepto de Opendata y Smartcity, tratando de aunar en una única web la información relativa al tráfico 
        en tiempo real, la contaminación acústica en tiempo real y la contaminación atmosférica junto con la información relativa a las mejores  
        rutas recomendadas desde el Ayuntamiento de Madrid para los desplazamientos en bicicleta. Asimismo, se puede planificar una ruta en bici 
        utilizando el planificador de rutas de Google, visualizando el usuario las calles por las que debe transitar, pero pudiendo elegir él mismo
        qué calles le convienen más, en función de la información en tiempo real que desde esta aplicación se proporciona.
            </div>
        </div>
    </div>
</asp:Content>
