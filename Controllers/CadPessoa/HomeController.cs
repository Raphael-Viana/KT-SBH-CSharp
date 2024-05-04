using KTCSharpApi.Repositories.Interfaces;
using KTCSharpApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KTCSharpApi.Controllers.CadPessoa
{
    [ApiController, Route("api/cad-pessoa")]
    public class HomeController : ControllerBase
    {
        public HomeController(ICadPessoaRepository repository)
        {
            _repository = repository;
        }

        private readonly ICadPessoaRepository _repository;

        [HttpPost, Route("search/{value}")]
        public async Task<IActionResult> SearchAsync(string value, CancellationToken cancellationToken) =>
            Ok(await _repository.GetBySearchAsync(value, cancellationToken));

        [HttpPost, Route("create")]
        public async Task<IActionResult> InsertAsync(CadPessoaViewModel viewModel, CancellationToken cancellationToken)
        {
            var cadPessoa = new Models.CadPessoa(nome: viewModel.Nome!,
                idade: viewModel.Idade);

            cadPessoa.AlterarPeso(viewModel.Peso);
            cadPessoa.AlterarAltura(viewModel.Altura);

            if (!viewModel.Profissoes.Any())
                return ValidationProblem("Necessário uma profissão.");

            foreach (var profissao in viewModel.Profissoes)
            {
                cadPessoa.AddProfissao(new Models.CadPessoa.CProfissao(cargoNome: profissao.CargoNome!,
                    empresaCnpj: profissao.EmpresaCnpj!,
                    empresaNome: profissao.EmpresaNome!));
            }

            await _repository.InsertAsync(cadPessoa, cancellationToken);
            return Ok("Pessoa cadastrada com sucesso!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CadPessoaViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!viewModel.Id.HasValue)
                return ValidationProblem("Registro não foi encontrado.");

            var cadPessoa = await _repository.GetAsync(viewModel.Id.Value, cancellationToken);
            
            cadPessoa.AlterarIdade(viewModel.Idade);
            cadPessoa.AlterarPeso(viewModel.Peso);
            cadPessoa.AlterarAltura(viewModel.Altura);

            if (!viewModel.Profissoes.Any())
                return ValidationProblem("Necessário uma profissão.");

            foreach (var profissao in viewModel.Profissoes)
            {
                cadPessoa.AddProfissao(new Models.CadPessoa.CProfissao(cargoNome: profissao.CargoNome!,
                    empresaCnpj: profissao.EmpresaCnpj!,
                    empresaNome: profissao.EmpresaNome!));
            }

            await _repository.UpdateAsync(cadPessoa, cancellationToken);
            return Ok("Cadastro atualizado com sucesso!");
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
            return Ok("Registro excluido com sucesso!");
        }
    }
}
