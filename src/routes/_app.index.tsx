import { createFileRoute } from "@tanstack/react-router";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { PageHeader } from "@/components/qhse/PageHeader";
import { SeverityBadge, StatusBadge } from "@/components/qhse/StatusBadge";
import { AlertTriangle, ClipboardCheck, FileWarning, ShieldAlert, TrendingDown, TrendingUp } from "lucide-react";
import {
  ResponsiveContainer,
  AreaChart,
  Area,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  PieChart,
  Pie,
  Cell,
  Legend,
} from "recharts";
import { incidents, incidentsTrend, severityDistribution } from "@/lib/qhse/mock-data";

export const Route = createFileRoute("/_app/")({
  component: Dashboard,
});

const kpis = [
  { label: "Incidents ouverts", value: "12", delta: "-8%", trend: "down" as const, icon: AlertTriangle, color: "text-destructive" },
  { label: "Audits planifiés", value: "7", delta: "+2", trend: "up" as const, icon: ClipboardCheck, color: "text-primary" },
  { label: "Non-conformités", value: "23", delta: "+4", trend: "up" as const, icon: FileWarning, color: "text-warning-foreground" },
  { label: "Risques critiques", value: "4", delta: "-1", trend: "down" as const, icon: ShieldAlert, color: "text-destructive" },
];

function Dashboard() {
  return (
    <div className="space-y-6">
      <PageHeader
        title="Tableau de bord"
        description="Vue consolidée de la performance QHSE de l'entreprise."
      />

      <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-4">
        {kpis.map((kpi) => {
          const Icon = kpi.icon;
          const TrendIcon = kpi.trend === "up" ? TrendingUp : TrendingDown;
          return (
            <Card key={kpi.label} className="shadow-[var(--shadow-card)]">
              <CardContent className="p-5">
                <div className="flex items-start justify-between">
                  <div>
                    <p className="text-sm text-muted-foreground">{kpi.label}</p>
                    <p className="mt-2 text-3xl font-semibold text-foreground">{kpi.value}</p>
                  </div>
                  <div className={`rounded-lg bg-secondary p-2 ${kpi.color}`}>
                    <Icon className="h-5 w-5" />
                  </div>
                </div>
                <div className="mt-3 flex items-center gap-1 text-xs">
                  <TrendIcon className={`h-3.5 w-3.5 ${kpi.trend === "down" ? "text-success" : "text-warning-foreground"}`} />
                  <span className="text-muted-foreground">{kpi.delta} vs mois dernier</span>
                </div>
              </CardContent>
            </Card>
          );
        })}
      </div>

      <div className="grid grid-cols-1 gap-6 lg:grid-cols-3">
        <Card className="lg:col-span-2 shadow-[var(--shadow-card)]">
          <CardHeader>
            <CardTitle className="text-base">Évolution des incidents</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="h-64">
              <ResponsiveContainer width="100%" height="100%">
                <AreaChart data={incidentsTrend} margin={{ top: 10, right: 10, left: -10, bottom: 0 }}>
                  <defs>
                    <linearGradient id="g1" x1="0" y1="0" x2="0" y2="1">
                      <stop offset="0%" stopColor="oklch(0.58 0.16 235)" stopOpacity={0.4} />
                      <stop offset="100%" stopColor="oklch(0.58 0.16 235)" stopOpacity={0} />
                    </linearGradient>
                    <linearGradient id="g2" x1="0" y1="0" x2="0" y2="1">
                      <stop offset="0%" stopColor="oklch(0.62 0.15 155)" stopOpacity={0.3} />
                      <stop offset="100%" stopColor="oklch(0.62 0.15 155)" stopOpacity={0} />
                    </linearGradient>
                  </defs>
                  <CartesianGrid strokeDasharray="3 3" stroke="oklch(0.92 0.01 245)" />
                  <XAxis dataKey="month" stroke="oklch(0.5 0.03 250)" fontSize={12} />
                  <YAxis stroke="oklch(0.5 0.03 250)" fontSize={12} />
                  <Tooltip contentStyle={{ borderRadius: 8, border: "1px solid oklch(0.92 0.01 245)" }} />
                  <Area type="monotone" dataKey="incidents" stroke="oklch(0.58 0.16 235)" fill="url(#g1)" strokeWidth={2} />
                  <Area type="monotone" dataKey="resolved" stroke="oklch(0.62 0.15 155)" fill="url(#g2)" strokeWidth={2} />
                </AreaChart>
              </ResponsiveContainer>
            </div>
          </CardContent>
        </Card>

        <Card className="shadow-[var(--shadow-card)]">
          <CardHeader>
            <CardTitle className="text-base">Répartition par sévérité</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="h-64">
              <ResponsiveContainer width="100%" height="100%">
                <PieChart>
                  <Pie data={severityDistribution} dataKey="value" nameKey="name" innerRadius={50} outerRadius={80} paddingAngle={2}>
                    {severityDistribution.map((entry) => (
                      <Cell key={entry.name} fill={entry.color} />
                    ))}
                  </Pie>
                  <Legend iconType="circle" wrapperStyle={{ fontSize: 12 }} />
                  <Tooltip />
                </PieChart>
              </ResponsiveContainer>
            </div>
          </CardContent>
        </Card>
      </div>

      <Card className="shadow-[var(--shadow-card)]">
        <CardHeader>
          <CardTitle className="text-base">Incidents récents</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-border text-left text-xs uppercase tracking-wider text-muted-foreground">
                  <th className="pb-2 pr-4 font-medium">ID</th>
                  <th className="pb-2 pr-4 font-medium">Titre</th>
                  <th className="pb-2 pr-4 font-medium">Site</th>
                  <th className="pb-2 pr-4 font-medium">Sévérité</th>
                  <th className="pb-2 pr-4 font-medium">Statut</th>
                  <th className="pb-2 font-medium">Date</th>
                </tr>
              </thead>
              <tbody>
                {incidents.slice(0, 5).map((inc) => (
                  <tr key={inc.id} className="border-b border-border/50 last:border-0">
                    <td className="py-3 pr-4 font-mono text-xs text-muted-foreground">{inc.id}</td>
                    <td className="py-3 pr-4 font-medium text-foreground">{inc.title}</td>
                    <td className="py-3 pr-4 text-muted-foreground">{inc.site}</td>
                    <td className="py-3 pr-4"><SeverityBadge severity={inc.severity} /></td>
                    <td className="py-3 pr-4"><StatusBadge status={inc.status} /></td>
                    <td className="py-3 text-muted-foreground">{inc.date}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </CardContent>
      </Card>
    </div>
  );
}