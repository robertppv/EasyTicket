import axios from 'axios';
import { useEffect, useState } from 'react';
function Dashboard({ user }) {
  const [tickets, setTickets] = useState(null);
  useEffect(() => {
    const fetchTickets = async () => {
      try {
        const token = localStorage.getItem('token');
        const res = await axios.get(`/api/Tickets/${user.id}/tickets`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        setTickets(res.data.$values);
        console.log(res.data.$values);
      } catch (err) {
        console.log(err);
      }
    };
    fetchTickets();
  }, []);

  return (
    <div className='flex mx-10 mt-2 container'>
      <div className='text-2xl border-2 border-gray rounded h-screen container  '>
        My tickets
        <ul>
          {tickets &&
            tickets.map((ticket, index) => (
              <li key={index}>
                <h1>
                  {' '}
                  {ticket.title} {ticket.description}
                </h1>
              </li>
            ))}
        </ul>
      </div>
    </div>
  );
}

export default Dashboard;
