<template>
      <div class="container-fluid py-5 mt-5">
        <div class="container py-5" v-if="movie && !loading">
          <div class="row g-4">
            <div class="col-lg-6">
              <img :src="movie.posterUrl" class="img-fluid rounded w-100" alt="Movie Poster" />
            </div>
            <div class="col-lg-6">
              <p>{{ movie.title }}</p>
              <p class="text-muted">Thể loại: {{ movie.genre }}</p>
              <p class="text-muted">Khởi chiếu: {{ formatDate(movie.releaseDate) }}</p>
              <p class="text-muted">Thời lượng: {{ movie.durationMinutes }} phút</p>
              <p class="text-muted">Đánh giá: {{ movie.rating }}/10</p>
              <p>{{ movie.description }}</p>
              <a v-if="movie.trailerUrl" :href="movie.trailerUrl" target="_blank" class="btn btn-outline-primary">
                🎬 Xem Trailer
              </a>
              <div v-else>
                <p class="text-muted">Không có trailer cho phim này.</p>
              </div>
              <p class="btn btn-secondary bi me-3 mt-4 " @click="$router.go(-1)">Quay lại</p>
            </div>
          </div>
        </div>
        <div v-else-if="loading" class="text-center">Đang tải...</div>
        <div v-else class="text-center text-danger">{{ errorMessage }}</div>
      </div>
  </template>
  <script setup>
  import { ref, onMounted } from 'vue';
  import { useRoute } from 'vue-router';
  import ProductService from '../../../service/productService';
  
  const route = useRoute();
  const movie = ref(null);  // Đảm bảo rằng bạn đang sử dụng ref
  const loading = ref(true);
  const errorMessage = ref('');
  
  // Hàm định dạng ngày
  const formatDate = (dateStr) => new Date(dateStr).toLocaleDateString('vi-VN');
  
  // Sử dụng onMounted để tải dữ liệu phim
  onMounted(async () => {
    const id = route.params.id;
    try {
      movie.value = await ProductService.getMovieById(id);  // Gọi API và gán dữ liệu vào movie.value
      console.log('Movie data:', movie.value);  // Kiểm tra dữ liệu phim
    } catch (err) {
      console.error('Lỗi khi lấy chi tiết phim:', err);
      errorMessage.value = 'Không thể tải thông tin phim.';
    } finally {
      loading.value = false;
    }
  });
  </script>
  