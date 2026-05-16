export type Severity = "low" | "medium" | "high" | "critical";
export type Status = "open" | "in_progress" | "resolved" | "closed";

export interface Incident {
  id: string;
  title: string;
  site: string;
  severity: Severity;
  status: Status;
  reportedBy: string;
  date: string;
}

export interface Audit {
  id: string;
  reference: string;
  scope: string;
  auditor: string;
  date: string;
  score: number;
  status: "planned" | "ongoing" | "completed";
}

export interface NonConformity {
  id: string;
  title: string;
  source: string;
  severity: Severity;
  owner: string;
  dueDate: string;
  status: Status;
}

export interface Risk {
  id: string;
  hazard: string;
  category: "Qualité" | "Hygiène" | "Sécurité" | "Environnement";
  likelihood: number;
  impact: number;
  mitigation: string;
}

export interface Report {
  id: string;
  name: string;
  type: string;
  uploadedBy: string;
  uploadedAt: string;
  sizeKb: number;
}

export const incidents: Incident[] = [
  { id: "INC-2041", title: "Fuite chimique zone B", site: "Usine Lyon", severity: "critical", status: "in_progress", reportedBy: "M. Dupont", date: "2025-05-14" },
  { id: "INC-2040", title: "Chute de plain-pied entrepôt", site: "Entrepôt Lille", severity: "medium", status: "resolved", reportedBy: "S. Martin", date: "2025-05-12" },
  { id: "INC-2039", title: "Non-port d'EPI ligne 3", site: "Usine Lyon", severity: "low", status: "closed", reportedBy: "A. Bernard", date: "2025-05-10" },
  { id: "INC-2038", title: "Émissions au-dessus du seuil", site: "Site Marseille", severity: "high", status: "open", reportedBy: "J. Petit", date: "2025-05-09" },
  { id: "INC-2037", title: "Court-circuit armoire électrique", site: "Usine Lyon", severity: "high", status: "in_progress", reportedBy: "L. Robert", date: "2025-05-07" },
  { id: "INC-2036", title: "Déversement huile atelier", site: "Atelier Nantes", severity: "medium", status: "resolved", reportedBy: "C. Moreau", date: "2025-05-05" },
];

export const audits: Audit[] = [
  { id: "AUD-118", reference: "ISO 45001 - T2", scope: "Sécurité - Usine Lyon", auditor: "Cabinet Veritas", date: "2025-06-02", score: 87, status: "planned" },
  { id: "AUD-117", reference: "ISO 14001 interne", scope: "Environnement - Marseille", auditor: "É. Lambert", date: "2025-05-15", score: 92, status: "completed" },
  { id: "AUD-116", reference: "ISO 9001 surveillance", scope: "Qualité - Tous sites", auditor: "Bureau AFNOR", date: "2025-05-08", score: 78, status: "completed" },
  { id: "AUD-115", reference: "Audit interne hygiène", scope: "Hygiène - Cantine Lille", auditor: "P. Garcia", date: "2025-05-20", score: 0, status: "ongoing" },
];

export const nonConformities: NonConformity[] = [
  { id: "NC-512", title: "Procédure de cadenassage non appliquée", source: "AUD-117", severity: "high", owner: "Resp. Sécurité", dueDate: "2025-06-10", status: "open" },
  { id: "NC-511", title: "Étiquetage produit chimique manquant", source: "INC-2041", severity: "critical", owner: "Resp. HSE", dueDate: "2025-05-30", status: "in_progress" },
  { id: "NC-510", title: "Registre des déchets incomplet", source: "AUD-116", severity: "medium", owner: "Resp. Environnement", dueDate: "2025-06-15", status: "open" },
  { id: "NC-509", title: "Formation EPI non renouvelée", source: "AUD-116", severity: "low", owner: "RH", dueDate: "2025-07-01", status: "resolved" },
];

export const risks: Risk[] = [
  { id: "R-001", hazard: "Exposition produits chimiques", category: "Sécurité", likelihood: 4, impact: 5, mitigation: "EPI + ventilation + formation trimestrielle" },
  { id: "R-002", hazard: "TMS opérateurs ligne d'assemblage", category: "Hygiène", likelihood: 3, impact: 3, mitigation: "Rotation des postes + étude ergonomique" },
  { id: "R-003", hazard: "Rejet aqueux hors normes", category: "Environnement", likelihood: 2, impact: 5, mitigation: "Contrôle continu + station d'épuration" },
  { id: "R-004", hazard: "Non-conformité produit fini", category: "Qualité", likelihood: 3, impact: 4, mitigation: "Contrôle qualité renforcé en sortie de ligne" },
  { id: "R-005", hazard: "Incendie zone stockage", category: "Sécurité", likelihood: 2, impact: 5, mitigation: "Sprinklers + audit annuel SSI" },
  { id: "R-006", hazard: "Contamination microbiologique", category: "Hygiène", likelihood: 2, impact: 4, mitigation: "Plan de nettoyage + prélèvements" },
];

export const reports: Report[] = [
  { id: "RPT-088", name: "Rapport mensuel HSE - Avril 2025", type: "PDF", uploadedBy: "É. Lambert", uploadedAt: "2025-05-02", sizeKb: 842 },
  { id: "RPT-087", name: "Audit ISO 14001 - Marseille", type: "PDF", uploadedBy: "Bureau AFNOR", uploadedAt: "2025-05-16", sizeKb: 1204 },
  { id: "RPT-086", name: "Analyse incidents Q1", type: "DOCX", uploadedBy: "S. Martin", uploadedAt: "2025-04-12", sizeKb: 332 },
  { id: "RPT-085", name: "Plan d'actions correctives", type: "XLSX", uploadedBy: "Resp. HSE", uploadedAt: "2025-04-08", sizeKb: 188 },
];

export const incidentsTrend = [
  { month: "Jan", incidents: 12, resolved: 9 },
  { month: "Fév", incidents: 9, resolved: 8 },
  { month: "Mar", incidents: 14, resolved: 11 },
  { month: "Avr", incidents: 7, resolved: 7 },
  { month: "Mai", incidents: 11, resolved: 6 },
];

export const severityDistribution = [
  { name: "Critique", value: 4, color: "oklch(0.6 0.22 25)" },
  { name: "Élevée", value: 9, color: "oklch(0.75 0.16 75)" },
  { name: "Moyenne", value: 14, color: "oklch(0.58 0.16 235)" },
  { name: "Faible", value: 18, color: "oklch(0.62 0.15 155)" },
];