<%@ Page Title="" Language="C#" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="AMALIA.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BODY" runat="server">
    <section class="content">
        <div class="container">
            <div runat="server" id="DIV_DASH" visible="false">
                <div class="row clearfix">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="body block-header">
                                <div class="row">
                                    <div class="col-lg-6 col-md-8 col-sm-12">
                                        <h2>Dashboard Mes: <span runat="server" id="LBL_ANO"></span></h2>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row clearfix">
                    <div class="col-sm-6">
                        <div class="card">
                            <div class="header">
                                <h2>Resumen<strong> GT</strong></h2>
                                <ul class="header-dropdown">
                                    <li class="remove">
                                        <a role="button" class="boxs-close"><i class="zmdi zmdi-close"></i></a>
                                    </li>
                                </ul>
                            </div>
                            <div class="body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <h2 class="m-b-0"><span runat="server" id="L_SALDO_TOTAL"></span></h2>
                                        <p>Ganancias del Mes</p>
                                    </div>
                                </div>
                                <div class="table-responsive">
                                    <table class="table table-hover m-b-0">
                                        <tbody>
                                            <tr>
                                                <th><i class="zmdi zmdi-circle text-success"></i></th>
                                                <td>Total Fletes</td>
                                                <td class="text-success font-bold"><span runat="server" id="L_FLETE"></span></td>
                                            </tr>
                                            <tr>
                                                <th><i class="zmdi zmdi-circle text-info"></i></th>
                                                <td>Carga Combustible</td>
                                                <td class="text-info font-bold"><span runat="server" id="L_COMBUSTIBLE"></span></td>
                                            </tr>
                                            <tr>
                                                <th><i class="zmdi zmdi-circle text-warning"></i></th>
                                                <td>Gastos Generales</td>
                                                <td class="text-warning font-bold"><span runat="server" id="L_GASTO_GENERAL"></span></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="card info-box-2 text-center">
                                    <div class="body">
                                        <div class="content text-center">
                                            <i class="fa fa-shipping-fast fa-2x col-amber"></i>
                                            <div runat="server" id="L_KM_RECORRIDOS"></div>
                                            <div class="text">Kilometros</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="card info-box-2 text-center">
                                    <div class="body">
                                        <div class="content text-center">
                                            <i class="fa fa-gas-pump col-blue fa-2x"></i>
                                            <div runat="server" id="L_TOTAL_LITROS"></div>
                                            <div class="text">Litros Cargados</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                              <div class="col-sm-6">
                                <div class="card info-box-2 text-center">
                                    <div class="body">
                                        <div class="content text-center">
                                            <i class="fa fa-gas-pump col-green fa-2x"></i>
                                            <div runat="server" id="L_STOCK_ESTANQUE"></div>
                                            <div class="text">Stock Estanque </div>
                                        </div>
                                    </div>
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
</asp:Content>
