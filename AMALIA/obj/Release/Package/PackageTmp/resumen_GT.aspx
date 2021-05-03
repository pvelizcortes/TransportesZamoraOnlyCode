<%@ Page Title="" Language="C#" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="resumen_GT.aspx.cs" Inherits="AMALIA.resumen_GT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BODY" runat="server">
    <section class="content">
        <div class="container">
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="body block-header">
                            <div class="row">
                                <div class="col-lg-6 col-md-8 col-sm-12" runat="server" id="DIV_TITULO">                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row clear-fix">
                <div class="col-md-12 col-lg-12">
                    <div class="card">
                        <ul class="row profile_state list-unstyled">
                            <li class="col-sm-2">
                                <div class="body">
                                    <i class="fa fa-dollar-sign col-green"></i>
                                    <i class="fa fa-arrow-right col-green"></i>
                                      <i class="fa fa-user col-green"></i>
                                    <h4><span runat="server" id="DIV_DINERO_ENTREGADO"></span></h4>
                                    <span>Dinero Entregado</span>
                                </div>
                            </li>
                            <li class="col-sm-2">
                                <div class="body">
                                      <i class="fa fa-dollar-sign col-blue"></i>
                                    <i class="fa fa-arrow-left col-blue"></i>
                                      <i class="fa fa-user col-blue"></i>
                                    <h4><span runat="server" id="DIV_DINEROXENTREGAR"></span></h4>
                                    <span>Saldo</span>
                                </div>
                            </li>
                            <li class="col-sm-2">
                                <div class="body">
                                    <i class="fa fa-shipping-fast col-amber"></i>
                                    <h4><span runat="server" id="DIV_KM_RECORRIDOS"></span></h4>
                                    <span>Kms. Recorridos</span>
                                </div>
                            </li>
                            <li class="col-sm-2">
                                <div class="body">
                                    <i class="fa fa-gas-pump col-blue"></i>
                                    <h4><span runat="server" id="DIV_LITROS_CARGADOS"></span></h4>
                                    <span>Litros Cargados.</span>
                                </div>
                            </li>
                            <li class="col-sm-2">
                                <div class="body">
                                    <i class="fa fa-route col-green"></i>
                                    <h4><span runat="server" id="DIV_RENDIMIENTO"></span></h4>
                                    <span>Rendimiento</span>
                                </div>
                            </li>
                            <li class="col-sm-2">
                                <div class="body">
                                    <i class="fa fa-chart-line col-green"></i>
                                    <h4><span runat="server" id="DIV_SALDO_FINAL"></span></h4>
                                    <span><b>Saldo Final</b></span>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="row clearfix">
                <div class="col-lg-7">
                    <div class="card">
                        <div class="body widget-user">
                            <img class="rounded-circle" src="assets/images/profile_av.jpg" alt="">
                            <div class="wid-u-info">
                                <h5>Conductor: <span runat="server" id="DIV_NOMBRE_CONDUCTOR"></span></h5>
                                <p class="text-muted m-b-0">
                                  <b><span runat="server" id="DIV_OBSERVACION"></span></b>
                                </p>
                            </div>
                            <hr>
                            <div class="row">
                                <div class="col-3 text-center">
                                    <i class="fa fa-truck col-amber fa-2x"></i>
                                    <h5 class="m-b-0"><span runat="server" id="DIV_CAMION"></span></h5>
                                    <small>Camión</small>
                                </div>
                                 <div class="col-3 text-center">
                                    <i class="fa fa-pallet col-amber fa-2x"></i>
                                    <h5 class="m-b-0"><span runat="server" id="DIV_RAMPLA"></span></h5>
                                    <small>Rampla</small>
                                </div>
                                <div class="col-3 text-center">
                                    <i class="fa fa-calendar col-deep-purple fa-2x"></i>
                                    <h5 class="m-b-0"><span runat="server" id="DIV_INICIO"></span></h5>
                                    <small>Inicio</small>
                                </div>
                                <div class="col-3 text-center">
                                    <i class="fa fa-calendar col-deep-purple fa-2x"></i>
                                    <h5 class="m-b-0"><span runat="server" id="DIV_TERMINO"></span></h5>
                                    <small>Termino</small>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-5">
                    <div class="card">
                        <div class="body">
                            <div class="table-responsive">
                                <table class="table table-hover m-b-0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <h6 class="margin-0">Total Flete</h6>
                                                <span>Servicios OTZ</span>
                                            </td>
                                            <td class="text-right">
                                                <h4 class="margin-0 text-success"><span runat="server" id="DIV_TOTAL_FLETE"></span></h4>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h6 class="margin-0">Total Combustible</h6>
                                                <span>Cargas de Combustibles</span>
                                            </td>
                                            <td class="text-right">
                                                <h4 class="margin-0 text-danger"><span runat="server" id="DIV_TOTAL_COMBUSTIBLE"></span></h4>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h6 class="margin-0">Gastos Generales</h6>
                                                <span>Peajas, Viaticos, Cargas etc..</span>
                                            </td>
                                            <td class="text-right">
                                                <h4 class="margin-0 text-danger"><span runat="server" id="DIV_TOTAL_GASTOS"></span></h4>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
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
