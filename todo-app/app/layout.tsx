import './globals.css'

export const metadata = {
  title: 'todo App',
  description: 'todo App',
}

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode
}>) {
  return (
    <html lang="en">
      <body>{children}</body>
    </html>
  )
}
