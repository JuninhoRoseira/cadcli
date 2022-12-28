// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const baseApiUrl = "https://localhost:7117/home";
let items = [];

$(document).ready(function () {
    $(".alert-result").hide();
    $("#dataNascimento").datepicker();

    // https://vitalets.github.io/bootstrap-datepicker/
    $("#dataNascimentoButton").click(function (e) {
        e.preventDefault();

        $('#dataNascimento').datepicker('show');

    });

    $(".btn-novo").click(function (e) {
        e.preventDefault();

        $("#idCliente").val("");
        $("#documento").val("");
        $("#nome").val("");
        $("#dataNascimento").datepicker('update', "")
        $("#endereco").val("");
        $("#cidade").val("");
        $('#estado').val("").change();
        $('input:radio[name="sexo"]').attr('checked', false);
        $(".btn-novo").text("Novo");

    });

    $(".btn-salvar").click(function (e) {
        e.preventDefault();

        var cliente = createClient();
        var valido = validateClient(cliente);

        if (valido) {
            $.post(baseApiUrl + '/salvarcliente', cliente, function (data) {
                if (cliente.id < 1) {
                    addTableRow(data);
                } else {
                    itemNovo = createTableRow(data);

                    var i = items.findIndex((c => c.indexOf("cliente_" + data.id) > -1));

                    items[i] = itemNovo;

                }

                $(".clientes-table tbody")
                    .empty()
                    .append(items);

                verifyTableItems();

            });

        }

    });

    $(document).on('click', '.tr-select-client', function (e) {
        e.preventDefault();

        var cliente = $(this).data("cliente-selecionado");

        setClient(cliente);

        $(".btn-novo").text("Cancelar");

    });

    $.getJSON(baseApiUrl + "/obterclientes", function (data) {
        items = [];

        $.each(data, function (index, cliente) {
            addTableRow(cliente);
        });

        $(".clientes-table tbody")
            .empty()
            .append(items);

        verifyTableItems();

        //$("").click(function (e) { });

    });

});

var exampleModal = document.getElementById('exampleModal');

exampleModal.addEventListener('show.bs.modal', function (event) {
    var button = event.relatedTarget;
    var jsonClient = button.getAttribute('data-bs-whatever');
    var recipient = JSON.parse(jsonClient);
    var modalTitle = exampleModal.querySelector('.modal-title');
    var modalBodyInput = exampleModal.querySelector('.modal-body');

    modalTitle.textContent = 'Deseja excluir cliente?';
    modalBodyInput.textContent = recipient.nome;

    $(".btn-modal-yes-clicked").unbind("click").on("click", function (e) {
        $.ajax({
            url: baseApiUrl + '/excluircliente?id=' + recipient.id,
            type: 'GET',
            success: function (result) {
                var resultClass = result.erro
                    ? "alert-danger"
                    : "alert-success";

                var $alertResult = $(".alert-result");

                $alertResult
                    .removeClass('alert-success')
                    .removeClass('alert-danger')
                    .addClass(resultClass)
                    .empty()
                    .text(result.mensagem)
                    .show();

                items = items.filter(i => i.indexOf("cliente_" + recipient.id) < 0);

                verifyTableItems();

                setTimeout(function () {
                    $alertResult.hide();
                    $(button).parent().parent().remove();
                }, 3000);

            }

        });

    });

});

function verifyTableItems() {
    $clientesTable = $(".clientes-table");

    if (!items.length) {
        $clientesTable.hide();
    } else {
        $clientesTable.show();
    }

}

function addTableRow(cliente) {
    items.push(createTableRow(cliente));
}

function createTableRow(cliente) {
    return "<tr class='tr-select-client' id='cliente_" + cliente.id + "' data-cliente-selecionado='" + JSON.stringify(cliente) + "'>" +
        "<td>" + cliente.id + "</td>" +
        "<td>" + cliente.documento + "</td>" +
        "<td>" + cliente.nome + "</td>" +
        "<td>" + cliente.dataNascimento + "</td>" +
        "<td>" + cliente.sexo + "</td>" +
        "<td>" + cliente.endereco + "</td>" +
        "<td>" + cliente.cidade + "</td>" +
        "<td>" + cliente.estado + "</td>" +
        "<td><button type='button' class='btn' data-bs-toggle='modal' data-bs-target='#exampleModal' data-bs-whatever='" + JSON.stringify(cliente) + "'><i class='bi bi-trash'></i></button></td>" +
        "</tr>";
}

function validateClient(cliente) {
    var errors = "";

    if (!cliente.nome.length) {
        errors += "<li>Nome é obrigatório</li>";
    }

    if (!cliente.documento.length) {
        errors += "<li>Documento é obrigatório</li>";
    }

    if (cliente.sexo == undefined || !cliente.sexo.length) {
        errors += "<li>Sexo é obrigatório</li>";
    }

    if (!cliente.cidade.length) {
        errors += "<li>Cidade é obrigatória</li>";
    }

    if (!cliente.estado.length) {
        errors += "<li>Estado é obrigatório</li>";
    }

    if (!cliente.dataNascimento.length) {
        errors += "<li>Data de nascimento é obrigatória</li>";
    }

    if (!cliente.endereco.length) {
        errors += "<li>Endereco é obrigatório</li>";
    }

    var $alertResult = $(".alert-result");

    if (errors.length) {
        $alertResult
            .removeClass('alert-success')
            .removeClass('alert-danger')
            .addClass('alert-danger')
            .empty()
            .html("<ul>" + errors + "</ul>")
            .show();

        setTimeout(function () {
            $alertResult.hide();
        }, 3000);

    } else {
        $alertResult.hide();
    }

    return !errors.length;

}

function createClient() {
    var idCliente = $("#idCliente").val();
    var documento = $("#documento").val();
    var nome = $("#nome").val();
    var dataNascimento = $("#dataNascimento").val();
    var sexo = $("input[name='sexo']:checked").val();
    var endereco = $("#endereco").val();
    var cidade = $("#cidade").val();
    var estado = $("#estado option:selected").text();

    var cliente = {
        id: idCliente,
        documento: documento,
        nome: nome,
        dataNascimento: dataNascimento,
        sexo: sexo,
        endereco: endereco,
        cidade: cidade,
        estado: estado
    };

    return cliente;

}

function setClient(cliente) {
    $("#idCliente").val(cliente.id);
    $("#documento").val(cliente.documento);
    $("#nome").val(cliente.nome);
    $("#dataNascimento").datepicker('update', cliente.dataNascimento)
    $("#endereco").val(cliente.endereco);
    $("#cidade").val(cliente.cidade);
    $('#estado').val(cliente.estado).change();

    var $radios = $('input:radio[name="sexo"]');

    $radios
        .attr('checked', false);

    $radios
        .filter('[value="' + cliente.sexo + '"]')
        .attr('checked', true);

}