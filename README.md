# Teste Técnico Hiper

Este projeto é uma solução para o teste técnico da Hiper.

## Pré-requisitos

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/)

## Como rodar o projeto

1. Clone este repositório:

	```bash
	git clone https://github.com/seu-usuario/seu-repo.git
	cd seu-repo
	```

2. Execute o Docker Compose:

	```bash
	docker-compose up --build
	```

3. O projeto estará disponível conforme configurado no `docker-compose.yml`.

4. Para parar os containers:

	```bash
	docker-compose down
	```

## Observações

- Certifique-se de que as portas utilizadas no `docker-compose.yml` estejam livres.
- Para customizações, edite os arquivos de configuração em `src/WebApi/appsettings.json`.

---
