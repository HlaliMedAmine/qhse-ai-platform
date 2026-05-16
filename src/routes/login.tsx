import { createFileRoute, Link, useNavigate } from "@tanstack/react-router";
import { useState } from "react";
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { ShieldCheck } from "lucide-react";

export const Route = createFileRoute("/login")({
  component: LoginPage,
});

function LoginPage() {
  const navigate = useNavigate();
  const [email, setEmail] = useState("elise.lambert@qhse.io");
  const [password, setPassword] = useState("");

  const onSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    navigate({ to: "/" });
  };

  return (
    <div className="flex min-h-screen w-full">
      <div className="hidden flex-1 bg-gradient-to-br from-[oklch(0.22_0.04_250)] via-[oklch(0.28_0.06_245)] to-[oklch(0.42_0.14_245)] p-12 text-white lg:flex lg:flex-col lg:justify-between">
        <div className="flex items-center gap-2">
          <div className="flex h-9 w-9 items-center justify-center rounded-lg bg-white/15 font-bold backdrop-blur">Q</div>
          <span className="text-sm font-semibold">QHSE AI Platform</span>
        </div>
        <div className="max-w-md space-y-4">
          <ShieldCheck className="h-10 w-10 text-white/80" />
          <h2 className="text-3xl font-semibold leading-tight">Digitalisez votre démarche Qualité, Hygiène, Sécurité, Environnement.</h2>
          <p className="text-white/70">Centralisez vos incidents, audits et rapports. Laissez l'IA détecter les risques et suggérer les actions correctives.</p>
        </div>
        <p className="text-xs text-white/50">© 2025 QHSE AI · Tous droits réservés</p>
      </div>
      <div className="flex flex-1 items-center justify-center bg-background p-6">
        <Card className="w-full max-w-sm shadow-[var(--shadow-card)]">
          <CardContent className="p-8">
            <h1 className="text-2xl font-semibold text-foreground">Connexion</h1>
            <p className="mt-1 text-sm text-muted-foreground">Accédez à votre espace QHSE.</p>
            <form onSubmit={onSubmit} className="mt-6 space-y-4">
              <div className="space-y-1.5">
                <Label htmlFor="email">Email professionnel</Label>
                <Input id="email" type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
              </div>
              <div className="space-y-1.5">
                <Label htmlFor="password">Mot de passe</Label>
                <Input id="password" type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder="••••••••" />
              </div>
              <Button type="submit" className="w-full">Se connecter</Button>
            </form>
            <p className="mt-6 text-center text-xs text-muted-foreground">
              Pas de compte ? <Link to="/login" className="font-medium text-primary hover:underline">Contactez votre administrateur</Link>
            </p>
          </CardContent>
        </Card>
      </div>
    </div>
  );
}