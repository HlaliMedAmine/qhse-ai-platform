import { Bell, Search } from "lucide-react";
import { Input } from "@/components/ui/input";

export function Topbar() {
  return (
    <header className="flex h-16 items-center justify-between border-b border-border bg-card px-6">
      <div className="relative w-full max-w-md">
        <Search className="pointer-events-none absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
        <Input placeholder="Rechercher un incident, audit, rapport…" className="pl-9" />
      </div>
      <div className="flex items-center gap-4">
        <button className="relative rounded-md p-2 text-muted-foreground hover:bg-secondary hover:text-foreground">
          <Bell className="h-5 w-5" />
          <span className="absolute right-1.5 top-1.5 h-2 w-2 rounded-full bg-destructive" />
        </button>
        <div className="flex items-center gap-3">
          <div className="flex h-9 w-9 items-center justify-center rounded-full bg-gradient-to-br from-primary to-[var(--primary-glow)] text-sm font-semibold text-primary-foreground">
            EL
          </div>
          <div className="hidden text-sm sm:block">
            <p className="font-medium text-foreground">Élise Lambert</p>
            <p className="text-xs text-muted-foreground">Responsable HSE</p>
          </div>
        </div>
      </div>
    </header>
  );
}