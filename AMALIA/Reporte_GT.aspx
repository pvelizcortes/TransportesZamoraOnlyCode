<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Reporte_GT.aspx.cs" Inherits="AMALIA.Reporte_GT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BODY" runat="server">
    
    <section class="content">
        <div class="container">
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="body block-header">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <span style="font-size: large"><strong>Reporte GT</strong> </span>
                                    <ul class="breadcrumb p-l-0 p-b-0 float-right">
                                        <li class="breadcrumb-item"><a href="index.aspx"><i class="icon-home"></i></a></li>
                                        <li class="breadcrumb-item"><a href="index.aspx">Index</a></li>
                                        <li class="breadcrumb-item active">Reporte GT</li>
                                    </ul>
                                </div>
                            </div>
                            <hr />
                            <div class="row clearfix">
                                <div class="col-sm-6">
                                    <small>SELECCIONE UN REPORTE</small><br />
                                    <asp:DropDownList runat="server" ID="CB_REPORTE" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="CB_REPORTE_SelectedIndexChanged">
                                        <%--  <asp:ListItem Text="---------------------- GT --------------------" Value="99"></asp:ListItem>
                                        <asp:ListItem Text="GT - Detalle de Saldos (Cobrado y Por Cobrar)" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="GT - Conductores con mayores gastos" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="---------------------- OTZ -------------------" Value="99"></asp:ListItem>
                                        <asp:ListItem Text="OTZ - OTZ's por facturar" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="OTZ - Total Fletes (Por Facturar y Facturados)" Value="6"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    <br />
                                    <asp:LinkButton runat="server" ID="B_FILTRAR" ClientIDMode="AutoID" OnClick="B_FILTRAR_Click" CssClass="btn btn-primary btn-sm btn-round"><i class="fa fa-search"></i> GENERAR</asp:LinkButton>
                                </div>
                            </div>
                            <asp:Panel runat="server" ID="F_GT" Visible="false">
                                <div class="row clearfix">
                                    <div class="col-sm-2">
                                        <small><b>GT DESDE</b></small><br />
                                        <asp:TextBox runat="server" ID="T_FILTRA_GT" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <small><b>GT HASTA</b></small><br />
                                        <asp:TextBox runat="server" ID="T_FILTRA_GT2" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2" runat="server" id="DIV_FILTRO_COMBUSTIBLE" visible="false">
                                        <small><b>AÑO COMPLETO</b></small><br />
                                        <asp:LinkButton runat="server" ID="B_FILTRAR2" ClientIDMode="AutoID" OnClick="B_FILTRAR2_Click" CssClass="btn btn-primary btn-sm btn-round" OnClientClick="relojito(true);"><i class="fa fa-search"></i> GENERAR AÑO COMPLETO</asp:LinkButton>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="F_FECHAS" Visible="false">
                                <div class="row clearfix">
                                    <div class="col-sm-2">
                                        <small>DESDE</small><br />
                                        <asp:TextBox runat="server" ID="FILTRA_FECHA_DESDE" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <small>HASTA</small><br />
                                        <asp:TextBox runat="server" ID="FILTRA_FECHA_HASTA" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <hr />

                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                <div class="body table-responsive">
                                    <div runat="server" id="DIV_TABLA"></div>
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
            Datatables();
        });

        function Datatables() {
            $('#G_PRINCIPAL').DataTable({
                destroy: true,
                stateSave: true,
                dom: 'lBfrtip',
                buttons: [
                    'copy', 'print'
                ],
                "language": {
                    "url": "assets/Spanish.json"
                }
            });
        }

    </script>
</asp:Content>
