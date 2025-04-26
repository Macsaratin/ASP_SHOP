<template>
  <div>
    <Header />
  </div>
    <div class="container py-5">
      <h2 class="text-center mb-5">📍 Danh sách Rạp Phim</h2>
      <div class="row g-4">
        <div
          v-for="cinema in cinemas"
          :key="cinema.id"
          class="col-md-6 col-lg-4 col-xl-3"
        >
          <div class="card h-100 shadow-sm cinema-card">
            <div class="card-body d-flex flex-column">
              <h5 class="card-title">{{ cinema.name }}</h5>
              <p class="card-text">
                🏢 Địa chỉ: {{ cinema.address }}
              </p>
              <p class="card-text">
                📞 Số điện thoại: {{ cinema.phone }}
              </p>
              <button
                class="btn btn-outline-primary mt-auto"
                @click="goToCinemaDetail(cinema.id)"
              >
                Xem chi tiết
              </button>
            </div>
          </div>
        </div>
      </div>
    <Footer />
  
  </div>
  </template>
  
  <script setup>
  import { ref, onMounted } from 'vue';
  import { useRouter } from 'vue-router';
  import CinemaService from '../../../service/cinemaService';
  import Header from './Navbar.vue';
  import Footer from './footer.vue';
  
  const router = useRouter();
  const cinemas = ref([]);
  
  const fetchCinemas = async () => {
    try {
      cinemas.value = await CinemaService.getAllCinemas();
    } catch (error) {
      console.error('Error loading cinemas:', error);
    }
  };
  
  const goToCinemaDetail = (cinemaId) => {
    router.push(`/cinemas/${cinemaId}`);
  };
  
  onMounted(() => {
    fetchCinemas();
  });
  </script>
  
  <style scoped>
  .cinema-card {
    transition: transform 0.3s ease;
  }
  .cinema-card:hover {
    transform: translateY(-5px);
    box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1);
  }
  </style>
  