<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Reporte_OTZ.aspx.cs" Inherits="AMALIA.Reporte_OTZ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BODY" runat="server">
    
    <section class="content">
        <div class="container">
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="body block-header">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <span style="font-size: large"><strong>Reporte OTZ</strong> </span>
                                    <ul class="breadcrumb p-l-0 p-b-0 float-right">
                                        <li class="breadcrumb-item"><a href="index.aspx"><i class="icon-home"></i></a></li>
                                        <li class="breadcrumb-item"><a href="index.aspx">Index</a></li>
                                        <li class="breadcrumb-item active">Reporte OTZ</li>
                                    </ul>
                                </div>
                            </div>
                            <hr />

                            <div class="row clearfix">
                                <div class="col-sm-4">
                                    <small><b>POR FECHAS:</b></small><br />
                                    <small>(ingrese ambas fechas para generar)</small>
                                    <br />
                                </div>
                                <div class="col-sm-2">
                                    <small>DESDE</small><br />
                                    <asp:TextBox runat="server" ID="FILTRA_FECHA_DESDE" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <small>HASTA</small><br />
                                    <asp:TextBox runat="server" ID="FILTRA_FECHA_HASTA" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <br />
                                    <asp:LinkButton runat="server" ID="B_FILTRAR" ClientIDMode="AutoID" OnClientClick="loader();" OnClick="B_FILTRAR_Click" CssClass="btn btn-primary btn-sm btn-round"><i class="fa fa-save"></i> GENERAR</asp:LinkButton>
                                </div>
                            </div>
                            <hr />
                            <div class="row clearfix">
                                <div class="col-sm-4">
                                    <small><b>POR NUM OTZ:</b></small><br />
                                    <small>(ingrese ambos Nº OTZ para generar)</small>
                                    <br />
                                </div>
                                <div class="col-sm-2">
                                    <small>DESDE</small><br />
                                    <asp:TextBox runat="server" ID="FILTRA_OTZ_DESDE" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <small>HASTA</small><br />
                                    <asp:TextBox runat="server" ID="FILTRA_OTZ_HASTA" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <br />
                                    <asp:LinkButton runat="server" ID="B_FILTRAR_NUM_OTZ" ClientIDMode="AutoID" OnClientClick="loader();" OnClick="B_FILTRAR_NUM_OTZ_Click" CssClass="btn btn-primary btn-sm btn-round"><i class="fa fa-save"></i> GENERAR</asp:LinkButton>
                                </div>
                            </div>
                            <hr />
                            <div class="row clearfix">
                                <div class="col-sm-4">
                                    <small><b>TODAS LAS OTZ:</b></small><br />
                                    <br />
                                </div>
                                <div class="col-sm-4" runat="server">
                                    <asp:LinkButton runat="server" ID="B_FILTRAR2" ClientIDMode="AutoID" OnClick="B_FILTRAR2_Click" CssClass="btn btn-primary btn-sm btn-round" OnClientClick="loader();"><i class="fa fa-save"></i> GENERAR TODAS LAS OTZ</asp:LinkButton>
                                </div>
                            </div>
                            <hr />
                            <div class="row clearfix">
                                <div class="col-sm-12 text-center" id="mensaje_carga" style="display: none;">
                                    <b>Generando excel por favor espere unos segundos...</b>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CODIGO_JAVASCRIPT" runat="server">
    <script>
        $(document).ready(function () {
            relojito(false);
        });
        function loader() {
            document.getElementById('mensaje_carga').style.display = "inline";
        }
    </script>
</asp:Content>
