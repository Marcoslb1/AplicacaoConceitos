$(document).ready(function () {

    mudarPaginaCadastro();
    buscarCep();
    cadastrar();

});

function abre(titulo, descricao) {

    var modal = `<div class="modalConceitos" id="modalConceitos">
                    <div class="modalconteudo">
                        <span class="fechar">&times;</span>
                        <div class="headerModal">${(titulo)}</div>
                        <div class="conteudo">${descricao}</div>
                    </div>
                </div>`;

    $('body').append(modal);
    $("#modalConceitos").addClass('mostrar');
}

function mudarPaginaCadastro() {
    const $formSections = $('.form-section');
    const $radioButtons = $('input[type="radio"]');

    $formSections.eq(0).show(); // Mostra a primeira seção

    $radioButtons.on('change', function () {
        const selectedValue = $(this).val();

        $formSections.hide(); // Oculta todas as seções
        $('#' + selectedValue).show(); // Exibe a seção selecionada
    });

    //const formSections = document.querySelectorAll('.form-section');
    //const radioButtons = document.querySelectorAll('input[type="radio"]');

    //formSections[0].style.display = 'block';

    //radioButtons.forEach((radio) => {
    //    radio.addEventListener('change', (event) => {
    //        const selectedValue = event.target.value;

    //        formSections.forEach((section) => {
    //            section.style.display = 'none';
    //        });

    //        document.getElementById(selectedValue).style.display = 'block';
    //    });
    //});
}

function buscarCep() {
    let $cep = $('.CEP');
    $cep.bind("blur", function () {
        var $this = $(this);

        if ($(this).val() != "") {
            var cep = $(this).val();

            $.ajax({
                method: "POST",
                url: "/Usuario/buscarCep",
                data: JSON.stringify(cep),
                contentType: 'application/json', //formato que será enviado para o servidor
                //datatype:'json', formato que espero receber do servidor
                success: function (responseCorreios) {
                    alert('sucesso')
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert('erro')
                }
            });
        }

    });
}

function cadastrar() {
    $(".btn-cadastrar").click(function (e) {
        e.preventDefault();

        var camposPreenchidos = true;
        $(".campo-comum").each(function () {
            if ($(this).val() === "") {
                camposPreenchidos = false;
                return false;
            }
        });

        if (camposPreenchidos) {
            var formData = new FormData();

            $(".campo-comum").each(function () {
                formData.append($(this).attr("name"), $(this).val());
            });

            $.ajax({
                method: "POST",
                url: "usuario/cadastrar",
                data: formData,
                contentType: false,
                processData: false,
                success: function () {
                    alert('sucesso')
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert('erro')
                }
            });
        } else {
            abre("Atenção", "Preecha os campos em branco.")
        }

    });
}

