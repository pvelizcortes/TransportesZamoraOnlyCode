<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Ingresar_Requerimiento.aspx.cs" Inherits="AMALIA.Ingresar_Requerimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BODY" runat="server">
    
    <section class="content">
        <div class="container">
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="body block-header">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:UpdatePanel runat="server" ID="UP_NUEVO" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="B_NUEVO" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <span style="font-size: large"><strong>REQUERIMIENTOS</strong></span>
                                            &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:LinkButton runat="server" ClientIDMode="AutoID" ID="B_NUEVO" CssClass="btn btn-primary btn-sm" OnClick="B_NUEVO_Click"><i class="fa fa-file"></i>&nbsp;NUEVO REQUERIMIENTO</asp:LinkButton>
                                            <ul class="breadcrumb p-l-0 p-b-0 float-right">
                                                <li class="breadcrumb-item"><a href="index.aspx"><i class="icon-home"></i></a></li>
                                                <li class="breadcrumb-item"><a href="index.aspx">Index</a></li>
                                                <li class="breadcrumb-item active">Ingresar Requerimiento</li>
                                            </ul>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:UpdatePanel runat="server" ID="UP_PRINCIPAL" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="B_GUARDAR" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Panel runat="server" ID="PANEL_DETALLE1" Visible="true">
                                                <div class="body">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <h2>INGRESE SU <strong>REQUERIMIENTO</strong></h2>
                                                            <hr />
                                                            <form class="form-horizontal">
                                                                <div style="display: none">
                                                                    <asp:TextBox runat="server" ID="T_ID"></asp:TextBox>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Camión</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList ID="CB_CAMION" runat="server" CssClass="form-control combopro">
                                                                                <asp:ListItem Text="-- seleccione --" Value="0" Selected="True"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Rampla</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList ID="CB_RAMPLA" runat="server" CssClass="form-control combopro">
                                                                                <asp:ListItem Text="-- seleccione --" Value="0" Selected="True"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Conductor Solicitante</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList ID="CB_CONDUCTOR" runat="server" CssClass="form-control combopro">
                                                                                <asp:ListItem Text="-- seleccione --" Value="0" Selected="True"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Descripción</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox TextMode="MultiLine" Rows="3" runat="server" ID="T_DESCRIPCION" CssClass="form-control " placeholder="Describa su requerimiento aqui..." required></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Prioridad</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList ID="CB_PRIORIDAD" runat="server" CssClass="form-control combopro">
                                                                                <asp:ListItem Text="BAJA" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="NORMAL" Value="2" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Text="ALTA" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="URGENTE" Value="4"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <hr />
                                                                <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-raised btn-primary btn-round waves-effect" runat="server" ID="B_GUARDAR" OnClick="B_GUARDAR_Click" OnClientClick="relojito(true);"><i class="fa fa-save"></i>&nbsp; GUARDAR</asp:LinkButton>

                                                                <div class="row clearfix">
                                                                </div>
                                                            </form>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                </div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
            ComboPro();
        });

        function ComboPro() {
            $('.combopro').selectize();
        }

    </script>
</asp:Content>
