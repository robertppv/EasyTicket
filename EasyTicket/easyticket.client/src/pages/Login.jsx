import axios from 'axios';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
const Login = () => {
  const [formData, setFormData] = useState({
    email: '',
    password: '',
  });
  const navigate = useNavigate();
  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log(formData);
    try {
      const res = await axios.post('/api/Auth/login', formData);
      console.log(res.data);
      localStorage.setItem('token', res.data);
      navigate('/dashboard');
    } catch (err) {
      console.log(err);
    }
  };
  return (
    <div className='min-h-screen flex items-center justify-center bg-gray-100'>
      <div className='bg-white p-8 rounded-lg shadow-lg w-full max-w-md border border-gray-200'>
        <h2 className='text-2xl font-bold mb-6 text-center text-gray-800'>
          Login
        </h2>
        <form onSubmit={handleSubmit}>
          <div>
            <label className='block text-gray-600 text-sm font-medium mb-1'>
              Email
            </label>
            <input
              className='w-full p-3 border border-grey rounded-md focus:ring-blue-200 outline-none focus:border-blue-400'
              type='email'
              name='email'
              value={formData.email}
              onChange={handleChange}
              placeholder='Enter your email'
              required
            />
          </div>
          <div className='mb-6'>
            <label className='block text-gray-600 text-sm font-medium mb-1'>
              Password
            </label>
            <input
              className='w-full p-3 border border-grey rounded-md focus:ring-blue-200 outline-none focus:border-blue-400'
              type='password'
              name='password'
              value={formData.password}
              onChange={handleChange}
              placeholder='Enter your password'
              required
            />
          </div>
          <button
            className='w-full bg-blue-500 text-white
          p-3 rounded-md hover:bg-blue-600 font-medium cursor-pointer'
          >
            Login
          </button>
        </form>
      </div>
    </div>
  );
};

export default Login;
