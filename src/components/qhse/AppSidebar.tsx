import { Link, useRouterState } from "@tanstack/react-router";
import {
  LayoutDashboard,
  AlertTriangle,
  ClipboardCheck,
  FileWarning,
  ShieldAlert,
  FileText,
  Sparkles,
  Settings,
  LogOut,
} from "lucide-react";

const nav = [
  { to: "/", label: "Tableau de bord", icon: LayoutDashboard },
  { to: "/incidents", label: "Incidents", icon: AlertTriangle },
  { to: "/audits", label: "Audits", icon: ClipboardCheck },
  { to: "/nonconformities", label: "Non-conformités", icon: FileWarning },
  { to: "/risks", label: "Analyse de risques", icon: ShieldAlert },
  { to: "/reports", label: "Rapports", icon: FileText },
  { to: "/ai-assistant", label: "Assistant IA", icon: Sparkles },
] as const;

export function AppSidebar() {
  const pathname = useRouterState({ select: (s) => s.location.pathname });

  return (
    <aside className="hidden w-64 shrink-0 flex-col border-r border-sidebar-border bg-sidebar text-sidebar-foreground md:flex">
      <div className="flex h-16 items-center gap-2 border-b border-sidebar-border px-6">
        <div className="flex h-9 w-9 items-center justify-center rounded-lg bg-sidebar-primary text-sidebar-primary-foreground font-bold">
          Q
        </div>
        <div className="flex flex-col">
          <span className="text-sm font-semibold">QHSE AI</span>
          <span className="text-[10px] uppercase tracking-wider text-sidebar-foreground/60">Platform</span>
        </div>
      </div>

      <nav className="flex-1 space-y-1 overflow-y-auto px-3 py-4">
        {nav.map((item) => {
          const active = pathname === item.to;
          const Icon = item.icon;
          return (
            <Link
              key={item.to}
              to={item.to}
              className={`flex items-center gap-3 rounded-md px-3 py-2 text-sm font-medium transition-colors ${
                active
                  ? "bg-sidebar-primary text-sidebar-primary-foreground shadow-sm"
                  : "text-sidebar-foreground/80 hover:bg-sidebar-accent hover:text-sidebar-accent-foreground"
              }`}
            >
              <Icon className="h-4 w-4" />
              {item.label}
            </Link>
          );
        })}
      </nav>

      <div className="border-t border-sidebar-border p-3">
        <button className="flex w-full items-center gap-3 rounded-md px-3 py-2 text-sm text-sidebar-foreground/80 transition-colors hover:bg-sidebar-accent hover:text-sidebar-accent-foreground">
          <Settings className="h-4 w-4" />
          Paramètres
        </button>
        <Link
          to="/login"
          className="flex w-full items-center gap-3 rounded-md px-3 py-2 text-sm text-sidebar-foreground/80 transition-colors hover:bg-sidebar-accent hover:text-sidebar-accent-foreground"
        >
          <LogOut className="h-4 w-4" />
          Déconnexion
        </Link>
      </div>
    </aside>
  );
}