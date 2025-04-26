<template>
    <div class="container py-5">
      <h2 class="text-center mb-4">Chi tiết Rạp Phim</h2>
      <div v-if="cinema" class="card shadow-sm">
        <div class="card-body">
          <h3 class="card-title">{{ cinema.name }}</h3>
          <p class="card-text">🏢 Địa chỉ: {{ cinema.address }}</p>
          <p class="card-text">📞 Số điện thoại: {{ cinema.phone }}</p>
          <p class="card-text">🌍 Vị trí: {{ cinema.location }}</p>
          <h4 class="mt-4">Buổi Chiếu Phim</h4>
          <ul v-if="screenings.length > 0">
            <li v-for="screening in screenings" :key="screening.id">
              <strong>{{ screening.movieTitle }}</strong> - {{ formatDate(screening.date) }}
              <p class="mb-0">🕒 Thời gian: {{ screening.time }}</p>
              <p class="mb-0">🎬 Thể loại: {{ screening.genre }}</p>
            </li>
          </ul>
          <p v-else>Không có buổi chiếu phim nào tại rạp này.</p>
        </div>
      </div>
      <button class="btn btn-secondary mt-4" @click="goBack">Quay lại</button>
    </div>
  </template>
  
  <script setup>
  import { ref, onMounted } from 'vue';
  import { useRoute, useRouter } from 'vue-router';
  import CinemaService from '../../../service/cinemaService';
  
  const router = useRouter();
  const route = useRoute();
  
  const cinema = ref(null);
  const screenings = ref([]);
  
  // Fetch the cinema details based on the id
  const fetchCinemaDetail = async (cinemaId) => {
    try {
      cinema.value = await CinemaService.getCinemaById(cinemaId);
      screenings.value = await CinemaService.getScreeningsByCinema(cinemaId);
    } catch (error) {
      console.error('Error fetching cinema details:', error);
    }
  };
  
  // Go back to the previous page
  const goBack = () => {
    router.push('/cinemas'); // Assuming /cinemas is the cinema listing page
  };
  
  // Format date to a readable format
  const formatDate = (dateStr) => new Date(dateStr).toLocaleDateString('vi-VN');
  
  // Fetch cinema details when the component is mounted
  onMounted(() => {
    const cinemaId = route.params.id; // Assuming the cinema ID is in the URL
    fetchCinemaDetail(cinemaId);
  });
  </script>
  
  <style scoped>
  .card {
    transition: transform 0.3s ease;
  }
  .card:hover {
    transform: translateY(-5px);
    box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1);
  }
  </style>
  