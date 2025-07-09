import { jwtDecode } from 'jwt-decode';
import { useEffect, useState } from 'react';
import {
  Navigate,
  Route,
  BrowserRouter as Router,
  Routes,
} from 'react-router-dom';
import Navbar from './components/Navbar';
import Admin from './pages/Admin';
import Dashboard from './pages/Dashboard';
import Home from './pages/Home';
import Login from './pages/Login';
import Register from './pages/Register';
const App = () => {
  const [user, setUser] = useState({
    id: '',
    name: '',
    role: '',
  });

  useEffect(() => {
    const fetch = () => {
      const token = localStorage.getItem('token');

      console.log(token);
      if (token) {
        const data = jwtDecode(token);
        const roleClaimName =
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
        const nameClaimName =
          'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name';
        const idClaimName =
          'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier';
        setUser({
          id: 'pula',
          name: data[nameClaimName],
          role: data[roleClaimName],
        });
        console.log(
          data[idClaimName],
          data[roleClaimName],
          data[nameClaimName]
        );
        console.log(user);
        console.log(user);
      }
    };
    fetch();
  }, []);
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route path='/' element={<Home />} />
        <Route
          path='/login'
          element={user.id ? <Navigate to='/dashboard' /> : <Login />}
        />
        <Route
          path='/register'
          element={user.id == '' ? <Navigate to='/dashboard' /> : <Register />}
        />
        <Route path='/admin' element={<Admin />} />
        <Route
          path='/dashboard'
          element={user.id == '' ? <Dashboard /> : <Navigate to='/' />}
        />
      </Routes>
    </Router>
  );
};

export default App;
