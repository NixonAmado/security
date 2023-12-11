ARG VARIANT=8.0-bookworm-slim
FROM mcr.microsoft.com/dotnet/sdk:${VARIANT}

RUN apt-get update && \
    apt-get install -y curl mariadb-client bat exa ca-certificates gnupg && \
    mkdir -p /etc/apt/keyrings && \
    curl -fsSL https://deb.nodesource.com/gpgkey/nodesource-repo.gpg.key | gpg --dearmor -o /etc/apt/keyrings/nodesource.gpg && \
    echo "deb [signed-by=/etc/apt/keyrings/nodesource.gpg] https://deb.nodesource.com/node_20.x nodistro main" > /etc/apt/sources.list.d/nodesource.list && \
    apt-get update && \
    apt-get install -y nodejs && \
    npm i npm@latest -g && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

ENV PATH $PATH:/home/vscode/.dotnet:/home/vscode/.dotnet/tools
