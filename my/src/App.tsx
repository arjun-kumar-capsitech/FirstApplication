import { Route, Routes } from 'react-router-dom'
import './App.css'
// import Nav from './Componants/Navbar'
import Login from './Componants/Login/Login'
import ProtectedRoute from './Componants/ProtectedRoute'
import Dashboard from './Componants/Dashboard/Dashboard'
import Index from './Componants/Index'
import Register from './Componants/Login/Register'

function App() {
  return (
    <>
      <Routes>
        <Route path="/" element={<Index />} />
        <Route path="/login" element={<Login />} />
          <Route path="/reg" element={<Register />} />
        <Route
          path="/dash2"
          element={
            <ProtectedRoute>
              <Dashboard />
            </ProtectedRoute>
          }
        />
      </Routes>
    </>
  )
}

export default App
