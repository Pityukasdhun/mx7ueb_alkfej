Alkalmazásfejlesztési Technológiák Projektfeladat
Készítette: Szkala István Balázs (MX7UEB)
Gábor Dénes Egyetem - mérnökinformatikus - távoktatás

A projekt egy mikroszolgáltatás alapú webalkalmazás, amely modern DevOps és GitOps eszközöket használ a build, tárolás és deploy automatizálására.
A rendszer egy egyszerű könyvkezelő alkalmazást valósít meg, amely több backend szolgáltatásból és egy Angular frontendből áll.

Architektúra
- Angular frontend
- ASP.NET Core microservice-ek
- MongoDB adatbázis
- Docker konténerek
- GitHub Actions CI pipeline
- GitHub Container Registry (GHCR)
- Kubernetes
- ArgoCD GitOps deploy

Frontend:
- Angular
- TypeScript
- Node.js

Backend:
- ASP.NET Core
- REST API

Adatbázis:
- MongoDB

Konténerizáció:
- Docker

CI/CD:
- GitHub Actions
- GitHub Container Registry (GHCR)

Orchestration:
- Kubernetes
- ArgoCD

Futtatás helyi (lokális/localhost) környezetben:
VS Code, Docker szoftverek használata előny

docker run -d -p 27017:27017 --name mx7ueb-mongo mongo

Book-Service:
cd services/book-service
dotnet run

User-Service:
cd services/user-service
dotnet run

MCP-Service:
cd services/mcp-server
dotnet run

Frontend:
cd frontend
npm install
npm start

UI elérhetősége: http://localhost:4200

Docker image-k:
ghcr.io/pityukasdhun/mx7ueb_alkfej/book-service
ghcr.io/pityukasdhun/mx7ueb_alkfej/user-service
ghcr.io/pityukasdhun/mx7ueb_alkfej/mcp-server
ghcr.io/pityukasdhun/mx7ueb_alkfej/frontend

Kubernetes deploy:
kubectl apply -f k8s

POD ellenőrzés:
kubectl get pods -n mx7ueb

ARGOCD felület:
http://localhost:8081

Application (alkalmazás):
mx7ueb-alkfej

CI/CD pipeline

A projekt automatikus build és deploy pipeline-t használ.
1.	GitHub push
2.	GitHub Actions build
3.	Docker image build
4.	GHCR push
5.	ArgoCD deploy
6.	Kubernetes rollout

Fejlesztési környezet

A projekt fejlesztése a következő környezetben történt:
macOS Tahoe
VS Code
Docker Desktop
Minikube
kubectl
ArgoCD

A projekt teljes mentése több formátumban készült:
ZIP projekt snapshot
ZIP projekt full-backup
Git bundle backup
Kubernetes cluster export

Licenc
Educational project – academic use