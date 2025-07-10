import axios from 'axios';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
const Register = () => {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    password: '',
    role: 'User',
  });

  const [showModal, setShowModal] = useState(false);

  const navigate = useNavigate();
  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log(formData);
    try {
      // eslint-disable-next-line no-unused-vars
      const res = await axios.post('/api/Auth/register', formData);

      setShowModal(true);
    } catch (err) {
      console.log(err);
    }
  };
  return (
    <div className='min-h-screen flex items-center justify-center'>
      <div className='bg-white p-8 rounded-lg shadow-lg w-full max-w-md border border-gray-200'>
        <h2 className='text-2xl font-bold mb-6 text-center text-gray-800'>
          Register
        </h2>
        <form onSubmit={handleSubmit}>
          <div>
            <label className='block text-gray-600 text-sm font-medium mb-1'>
              Name
            </label>
            <input
              className='w-full p-3 border border-grey rounded-md focus:ring-blue-200 outline-none focus:border-[#0A400C]'
              type='text'
              name='name'
              value={formData.name}
              onChange={handleChange}
              placeholder='Enter your name'
              required
            />
          </div>
          <div>
            <label className='block text-gray-600 text-sm font-medium mb-1'>
              Email
            </label>
            <input
              className='w-full p-3 border border-grey rounded-md focus:ring-blue-200 outline-none focus:border-[#0A400C]'
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
              className='w-full p-3 border border-grey rounded-md focus:ring-blue-200 outline-none focus:focus:border-[#0A400C]'
              type='password'
              name='password'
              value={formData.password}
              onChange={handleChange}
              placeholder='Enter your password'
              required
            />
          </div>
          <button
            className='w-full bg-[#0A400C] text-white
          p-3 rounded-md hover:bg-[#3f5b40] font-medium cursor-pointer'
          >
            Register
          </button>
        </form>
      </div>
      {showModal && (
        <div className='fixed bg-black/50 min-h-screen z-10 w-screen flex justify-center items-center top-0 left-0  '>
          <div className='bg-white p-4  shadow-lg rounded'>
            <div className='flex flex-col gap-4 max-w-[400px]'>
              <h2 className='font-bold text-3xl'>Thank you for registering!</h2>
              <p className='text-2xl'>Please sign in to your account.</p>
            </div>
            <div className='flex gap-4 mt-4'>
              <button
                onClick={() => navigate('/login')}
                className='bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600 '
              >
                Sign in
              </button>
              <button
                onClick={() => navigate('/')}
                className='bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600 '
              >
                Go Home
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default Register;
