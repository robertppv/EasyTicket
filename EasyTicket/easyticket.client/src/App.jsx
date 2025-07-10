import axios from 'axios';
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
  const [user, setUser] = useState(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetch = async () => {
      const token = localStorage.getItem('token');
      if (token) {
        try {
          const res = await axios.get('/api/Auth/get/user', {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          });
          setUser(res.data);
        } catch (err) {
          console.log(err);
          localStorage.clear('token');
        }
      }
      setIsLoading(false);
    };
    fetch();
  }, []);

  if (isLoading) {
    return <div>Loading....</div>;
  }
  return (
    <Router>
      <Navbar user={user} setUser={setUser} />
      <Routes>
        <Route path='/' element={<Home />} />
        <Route
          path='/login'
          element={
            user ? <Navigate to='/dashboard' /> : <Login setUser={setUser} />
          }
        />
        <Route
          path='/register'
          element={user ? <Navigate to='/dashboard' /> : <Register />}
        />
        <Route
          path='/admin'
          element={
            user ? (
              user.role === 'Admin' ? (
                <Admin />
              ) : (
                <Navigate to='/dashboard' />
              )
            ) : (
              <Navigate to='/' />
            )
          }
        />
        <Route
          path='/dashboard'
          element={
            user ? (
              user.role === 'User' ? (
                <Dashboard user={user} />
              ) : (
                <Navigate to='/admin' />
              )
            ) : (
              <Navigate to='/' />
            )
          }
        />
      </Routes>
    </Router>
  );
};

export default App;
