<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="NuevoReporteDepositos.aspx.cs" Inherits="AMALIA.NuevoReporteDepositos" %>

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
                                        <li class="breadcrumb-item active">Reporte Depositos</li>
                                    </ul>
                                </div>
                            </div>
                            <hr />
                            <div class="row clearfix">
                                <div class="col-sm-2">
                                    <small><b>GT DESDE</b></small><br />
                                    <asp:TextBox runat="server" ID="T_FILTRA_GT" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <small><b>GT HASTA</b></small><br />
                                    <asp:TextBox runat="server" ID="T_FILTRA_GT2" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <br />
                                    <asp:LinkButton runat="server" ID="B_FILTRAR" ClientIDMode="AutoID" OnClick="B_FILTRAR_Click" CssClass="btn btn-primary btn-sm btn-round"><i class="fa fa-search"></i> GENERAR</asp:LinkButton>
                                </div>
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
