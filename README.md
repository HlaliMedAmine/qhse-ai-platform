# QHSE AI Platform 🛡️

> Plateforme SaaS intelligente de gestion QHSE propulsée par Azure OpenAI, Microsoft Azure et .NET 8.

![Azure](https://img.shields.io/badge/Microsoft%20Azure-0078D4?style=for-the-badge&logo=microsoftazure&logoColor=white)
![.NET](https://img.shields.io/badge/.NET%208-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![React](https://img.shields.io/badge/React-61DAFB?style=for-the-badge&logo=react&logoColor=black)
![TypeScript](https://img.shields.io/badge/TypeScript-3178C6?style=for-the-badge&logo=typescript&logoColor=white)
![OpenAI](https://img.shields.io/badge/Azure%20OpenAI-412991?style=for-the-badge&logo=openai&logoColor=white)

---

# 📋 Présentation du projet

QHSE AI Platform est une plateforme SaaS moderne dédiée à la gestion :

- Qualité
- Hygiène
- Sécurité
- Environnement

Le projet combine une architecture Cloud Native basée sur Microsoft Azure avec l’intelligence artificielle Azure OpenAI afin d’automatiser l’analyse des incidents, des audits, des risques et des non-conformités.

---

# 🎯 Objectifs du projet

- Construire une architecture Cloud moderne sur Azure
- Intégrer Azure OpenAI dans une application métier réelle
- Automatiser l’analyse des incidents QHSE
- Déployer une application complète Frontend + Backend
- Mettre en place CI/CD avec GitHub Actions
- Utiliser Terraform comme Infrastructure as Code

---

# 🖥️ Interface utilisateur de la plateforme

L’application dispose d’une interface moderne permettant la gestion des incidents, audits, risques et analyses IA.

![QHSE Platform](./readme_images/01-platform-ui.png)

---

# 🤖 Assistant IA QHSE

L’assistant IA permet :

- L’analyse intelligente des incidents
- La classification automatique des risques
- La génération d’actions correctives
- La génération de recommandations préventives

### Exemple d’analyse IA

![AI Analysis](./readme_images/02-swagger-success.png)

---

# 🧠 Azure OpenAI Integration

Le projet utilise Azure OpenAI avec le modèle GPT-4o-mini déployé sur Azure AI Foundry.

### Déploiement Azure OpenAI

![Azure OpenAI](./readme_images/13-azure-openai.png)

---

# 🏗️ Architecture technique

## Frontend

- React
- TypeScript
- Vite
- Azure Static Web Apps

## Backend

- ASP.NET Core Web API (.NET 8)
- Swagger / OpenAPI
- Entity Framework Core
- Dependency Injection
- Repository Pattern

## Cloud & Infrastructure

- Microsoft Azure
- Azure App Service
- Azure Static Web Apps
- Azure OpenAI
- Azure SQL Database
- Terraform
- GitHub Actions

---

# 📂 Structure du projet

Le projet est organisé en plusieurs couches afin de respecter une architecture propre et scalable.

![Project Structure](./readme_images/07-project-structure.png)

---

# ☁️ Infrastructure as Code avec Terraform

L’infrastructure Azure a été provisionnée automatiquement avec Terraform.

### Terraform Apply

![Terraform](./readme_images/04-terraform-deploy.png)

### Ressources créées

- Azure Resource Group
- Azure App Service Plan
- Azure Linux Web App

---

# ⚙️ Build et publication du Backend

Le backend .NET 8 est compilé puis publié avant le déploiement sur Azure App Service.

### Dotnet Publish

![Dotnet Publish](./readme_images/05-dotnet-publish.png)

---

# 🚀 Déploiement Azure App Service

Le backend API est hébergé sur Azure App Service Linux.

### Azure App Service

![Azure App Service](./readme_images/11-azure-app-service.png)

---

# 🔍 Debugging et monitoring avec Kudu

Le projet utilise le portail Kudu pour le debugging, les logs et les vérifications runtime.

![Kudu Console](./readme_images/06-kudu-debug-console.png)

---

# 🌐 Déploiement Frontend Azure Static Web Apps

Le frontend React est automatiquement déployé sur Azure Static Web Apps.

![Azure Static Web Apps](./readme_images/08-azure-static-webapp.png)

---

# 🔄 CI/CD avec GitHub Actions

Le projet dispose d’un pipeline CI/CD automatisé.

### Workflows GitHub Actions

![GitHub Actions](./readme_images/03-github-actions.png)

### Déploiement automatique Frontend

![GitHub Deployment](./readme_images/09-github-deployment.png)

Fonctionnalités du pipeline :

- Build automatique
- Déploiement automatique
- Validation des commits
- Intégration continue
- Livraison continue

---

# 🧩 Implémentation du service IA

Le service IA backend est développé en C# avec Azure OpenAI SDK.

![AI Service Code](./readme_images/10-ai-service-code.png)

---

# 🧪 Swagger API Testing

L’API REST peut être testée directement via Swagger UI.

### Exemple de requête

![Swagger Request](./readme_images/02-swagger-success.png)

Fonctionnalités disponibles :

- Analyse IA
- Gestion des incidents
- Gestion des audits
- Gestion des risques
- Gestion des non-conformités

---

# 📌 Fonctionnalités principales

| Module | Description |
|--------|-------------|
| 🚨 Incidents | Gestion des incidents QHSE |
| 📋 Audits | Gestion des audits |
| ⚠️ Non-conformités | Suivi des non-conformités |
| 📊 Analyse des risques | Classification et évaluation |
| 🤖 Assistant IA | Analyse intelligente via Azure OpenAI |
| 📈 Rapports | Génération de rapports |

---

# 🌍 URLs de production

| Service | URL |
|---------|-----|
| Frontend | https://gentle-tree-0d42d1103.7.azurestaticapps.net |
| Backend API | https://qhse-api-dev-amine.azurewebsites.net |
| Swagger | https://qhse-api-dev-amine.azurewebsites.net/swagger |

---

# 🤖 Exemple de résultat IA

L’assistant IA analyse automatiquement les incidents QHSE et génère :

- Un résumé intelligent
- Un niveau de risque
- Des actions correctives
- Des recommandations préventives

## Exemple réel d’analyse

![Résultat IA](./readme_images/test.png)

---


# 📊 État actuel du projet

| Composant | Statut |
|-----------|--------|
| Frontend React | ✅ |
| Backend .NET 8 | ✅ |
| Swagger | ✅ |
| Azure OpenAI | ✅ |
| Terraform IaC | ✅ |
| GitHub Actions CI/CD | ✅ |
| Azure App Service | ✅ |
| Azure Static Web Apps | ✅ |

---

# 🎓 Compétences démontrées

- Microsoft Azure
- Azure OpenAI
- ASP.NET Core
- React + TypeScript
- Terraform
- CI/CD
- GitHub Actions
- REST API
- Swagger
- Infrastructure as Code
- Cloud Architecture
- DevOps

---

# 👨‍💻 Auteur

## Mohamed Amine Hlali

Senior Cloud & DevOps Engineer

### Certifications

- AZ-104
- AZ-305
- AZ-400
- Terraform Associate

GitHub : https://github.com/HlaliMedAmine

LinkedIn : https://linkedin.com/in/mohamedaminehlali

---

# 📄 Licence

Projet distribué sous licence MIT.
